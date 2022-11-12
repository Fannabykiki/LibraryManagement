using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Enums;
namespace BookStore.Data.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public UserRoleEnum Role { get; set; } //0. SuperUser 1. User
    }
}
