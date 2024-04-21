using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Inner
{
    public class CreateInvitationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public long Deadline { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation()
            {
                Email = this.Email,
                Name = this.Name,
                Deadline = this.Deadline,
            };
        }


    }
}