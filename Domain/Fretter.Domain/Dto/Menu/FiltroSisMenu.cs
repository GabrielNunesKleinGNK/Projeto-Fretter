using System;
namespace Fretter.Domain.Dto.Menu
{
    public class FiltroSisMenu
    {
        public FiltroSisMenu(int userId, bool isAdmin, bool isFretter)
        {
            UserId = userId;
            IsAdmin = isAdmin;
            IsFretter = isFretter;
        }

        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsFretter { get; set; }
    }
}
