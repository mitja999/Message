using MessageManagement.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageInfrastructure
{
    public class MessageDbContextInit
    {
        private readonly Dictionary<int, Message> Messages = new Dictionary<int, Message>();

        public static void Initialize(MessageDbContext context)
        {
            var init = new MessageDbContextInit();
            //load data and add to cache
            context.Messages.Load();
            context.Users.Load();
            context.MessageUser.Load();
            init.InsertDefaultData(context);
        }

        public void InsertDefaultData(MessageDbContext context)
        {
            context.Database.EnsureCreated();


            if (context.Messages.Count() == 0)
            {
                var messages = new[]
                {
                    new Message{Name="test",State=1,Template="Sample template",Type=1},
                    new Message{Name="test2",State=1,Template="Sample template2",Type=1}
                };
                context.Messages.AddRange(messages);
                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                var Users = new[]
                {
                    new User{Active=true,Email="test@gmail.com"},
                    new User{Active=true,Email="test2@gmail.com"},
                    new User{Active=true,Email="test3@gmail.com"}
                };

                context.Users.AddRange(Users);
                context.SaveChanges();
                var Users2 = new[]
                {
                    new User{Active=true,Email="test@gmail.com"},
                    new User{Active=true,Email="test2@gmail.com"},
                    new User{Active=true,Email="test3@gmail.com"}
                };

                context.Users.AddRange(Users2);
                context.SaveChanges();
            }

            if (context.MessageUser.Count() == 0)
            {
                var messageUser = new[]
                {
                    new MessageUser{MessageId=context.Messages.Find(2).Id,UserId=context.Users.Find(6).Id},
                    new MessageUser{MessageId=context.Messages.Find(2).Id,UserId=context.Users.Find(1).Id},
                    new MessageUser{MessageId=context.Messages.Find(2).Id,UserId=context.Users.Find(2).Id},
                    new MessageUser{MessageId=context.Messages.Find(2).Id,UserId=context.Users.Find(3).Id},
                    new MessageUser{MessageId=context.Messages.Find(1).Id,UserId=context.Users.Find(3).Id},
                    new MessageUser{MessageId=context.Messages.Find(1).Id,UserId=context.Users.Find(4).Id},
                    new MessageUser{MessageId=context.Messages.Find(1).Id,UserId=context.Users.Find(5).Id}
                };
                context.MessageUser.AddRange(messageUser);
                context.SaveChanges();
            }
        }
    }
}
