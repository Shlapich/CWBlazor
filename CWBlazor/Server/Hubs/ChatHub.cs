using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using CWBlazor.Domain;
using CWBlazor.Domain.Entities;
using CWBlazor.Server.Hubs.ChatModels;
using CWBlazor.Server.Hubs.ChatServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CWBlazor.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public const string HubUrl = "socket/chat";
        private const string UpdateErrorMessage = "Connections Dictionary was not updated.";
        private const string AddErrorMessage = "Value was not added to Connections Dictionary.";

        private readonly UserManager<CWUser> userManager;
        private readonly CWServerDbContext dbContext;

        /// <summary>
        /// Dictionary.
        /// </summary>
        public static readonly ConcurrentDictionary<string, Keys> Dictionary =
            new ConcurrentDictionary<string, Keys>(StringComparer.OrdinalIgnoreCase);

        public ChatHub(UserManager<CWUser> userManager, CWServerDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task Broadcast(string message)
        {
            var user = await userManager.GetUserAsync(Context.User);
            if (Dictionary.TryGetValue(user.Id, out var keys))
            {
                message = CryptoService.Decrypt(message, keys);
                await dbContext.Messages.AddAsync(new Message { Sender = user, Text = message });
                await dbContext.SaveChangesAsync();
                await Clients.All.SendAsync("Broadcast", CryptoService.Encrypt(user.Email, keys),
                    CryptoService.Encrypt(message, keys));
            }
        }

        public override async Task OnConnectedAsync()
        {
            var keys = CryptoService.GeneratePair();
            var userClaims = Context.User;
            var user = await userManager.GetUserAsync(userClaims);

            if (Dictionary.TryGetValue(user.Id, out var oldKeys))
            {
                if (Dictionary.TryUpdate(user.Id, keys, oldKeys))
                {
                    return;
                }

                throw new Exception(UpdateErrorMessage);
            }

            if (!Dictionary.TryAdd(user.Id, keys))
            {
                throw new Exception(AddErrorMessage);
            }
        }

        public async Task Load()
        {
            var user = await userManager.GetUserAsync(Context.User);
            if (Dictionary.TryGetValue(user.Id, out var keys))
            {
                foreach (var message in await dbContext.Messages.Include(m => m.Sender).ToListAsync())
                {
                    await Clients.Caller.SendAsync("Broadcast", CryptoService.Encrypt(message.Sender.Email, keys),
                        CryptoService.Encrypt(message.Text, keys));
                }
            }
        }

        public async Task UpdateSymmetricKey(byte[] encryptedKey)
        {
            var userClaims = Context.User;
            var user = await userManager.GetUserAsync(userClaims);
            if (Dictionary.TryGetValue(user.Id, out var keys))
            {
                var updatedKey = CryptoService.UpdateKeysBySymmetricKey(encryptedKey, keys);

                if (Dictionary.TryUpdate(user.Id, updatedKey, keys))
                {
                    return;
                }

                throw new Exception(UpdateErrorMessage);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userClaims = Context.User;
            var user = await userManager.GetUserAsync(userClaims);

            if (Dictionary.TryGetValue(user.Id, out _))
            {
                Dictionary.TryRemove(user.Id, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}