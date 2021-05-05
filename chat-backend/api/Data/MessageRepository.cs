using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Helpers;
using api.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void UpdateMessage(Message message)
        {
            _context.Messages.Update(message);
        }
    
        public async Task<Message> getMessage(int id)
        {
            return await _context.Messages
                .Include(u => u.Sender)
                .Include(u => u.Recipient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageList<MessageDto>> GetMessagesForUser(MessageParam messageParam)
        {
            var query = _context.Messages
                .OrderByDescending(MessageParam => MessageParam.MessageSent)
                .Include(s => s.Sender.Photos)
                .Include(r => r.Recipient.Photos)
                .AsSingleQuery()
                .AsQueryable();

            query = messageParam.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParam.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.UserName == messageParam.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.UserName == messageParam.Username && u.DateRead == null && u.RecipientDeleted == false)
            };

            var messages = query.Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                SenderUsername = m.SenderUsername,
                SenderPhotoUrl = m.Sender.Photos.FirstOrDefault(x => x.IsMain).Url,
                RecipientId = m.RecipientId,
                RecipientUsername = m.RecipientUsername,
                RecipientPhotoUrl = m.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url,
                Content = m.Content,
                DateRead = m.DateRead,
                MessageSent = m.MessageSent
            });

            return await PageList<MessageDto>.CreateAsync(messages, messageParam.pageNumber, messageParam.pageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                 .Include(u => u.Sender.Photos)
                 .Include(u => u.Recipient.Photos)
                 .Where(m => m.Recipient.UserName == currentUsername
                         && m.Sender.UserName == recipientUsername
                         && m.RecipientDeleted == false
                         || m.Recipient.UserName == recipientUsername
                         && m.Sender.UserName == currentUsername
                         && m.SenderDeleted == false
                 )
                 .OrderBy(m => m.MessageSent).AsSingleQuery()
                 .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.Recipient.UserName == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var m in unreadMessages)
                {
                    m.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
