using System;
namespace api.Helpers
{
    public class MessageParam
    {
        private int MaxPageSize = 50;
        private int _pageSize = 10;

        public int pageNumber { get; set; } = 1;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Username { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
