namespace CWBlazor.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public CWUser Sender { get; set; }

        public string SenderId { get; set; }
    }
}