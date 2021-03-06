
using System;
using VidsNet.Enums;

namespace VidsNet.Models
{
    public class SystemMessage {
        public int Id {get;set;}
        public int UserId {get;set;}
        public string Message {get;set;}
        public int Read {get;set;}
        public Severity Severity {get;set;}
        public DateTime Timestamp {get;set;}
        public string LongMessage {get;set;}
    }
}