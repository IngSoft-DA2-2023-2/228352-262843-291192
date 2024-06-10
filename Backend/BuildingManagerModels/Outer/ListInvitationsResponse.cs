using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ListInvitationsResponse
    {
        public List<Invitation> Invitations { get; set; }

        public ListInvitationsResponse(List<Invitation> invitations)
        {
            Invitations = invitations;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ListInvitationsResponse other = (ListInvitationsResponse)obj;
            return Invitations.SequenceEqual(other.Invitations);
        }
    }
}