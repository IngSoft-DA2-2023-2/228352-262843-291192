using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class ModifyInvitationRequest
    {
        private long _newDeadline;
        public ModifyInvitationRequest(long newDeadline)
        {
            NewDeadline = newDeadline;
            Validate();
        }
        public long NewDeadline { get { return _newDeadline; } set { _newDeadline = value; Validate(); } }

        public void Validate()
        {
            if (NewDeadline == 0)
            {
                throw new InvalidArgumentException("deadline");
            }
        }
    }
}