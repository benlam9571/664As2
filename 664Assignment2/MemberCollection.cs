using System;

namespace LibraryManagement
{
    public class MemberCollection
    {
        private CustomList<Member> members = new CustomList<Member>();

        public void AddMember(Member member)
        {
            members.Add(member);
        }

        public bool RemoveMember(string firstName, string lastName)
        {
            var member = members.Find(m => m.FirstName == firstName && m.LastName == lastName);
            if (member != null)
            {
                members.Remove(member);
                return true;
            }
            return false;
        }

        public Member FindMember(string firstName, string lastName, string password)
        {
            return members.Find(m => m.FirstName == firstName && m.LastName == lastName && m.Password == password);
        }
    }
}
