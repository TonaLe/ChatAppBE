using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Helpers;

namespace api.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);

        void DeleteMessage(Message message);

        void UpdateMessage(Message message);

        Task<Message> getMessage(int id);

        Task<PageList<MessageDto>> GetMessagesForUser(MessageParam messageParam);

        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);

        Task<bool> SaveAllAsync();
    }
}
