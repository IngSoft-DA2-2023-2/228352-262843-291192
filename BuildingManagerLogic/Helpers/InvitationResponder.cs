using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerLogic.Helpers
{
    public static class InvitationResponder
    {
        public static InvitationAnswer CreateAnswer(User user, Invitation invitation)
        {
            return new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Status = invitation.Status,
                Email = invitation.Email,
                Password = user?.Password,
            };
        }
    }
}
