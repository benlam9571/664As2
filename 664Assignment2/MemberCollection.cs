public class MemberCollection
{
    private Member[] members;
    private int memberCount;
    private const int InitialCapacity = 4;

    public MemberCollection()
    {
        members = new Member[InitialCapacity];
        memberCount = 0;
    }

    public bool AddMember(Member member)
    {
        // Check if the member already exists
        for (int i = 0; i < memberCount; i++)
        {
            if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
            {
                Console.WriteLine("Member already exists.");
                return false;
            }
        }

        // Resize array if necessary
        if (memberCount == members.Length)
        {
            ResizeArray();
        }

        members[memberCount] = member;
        memberCount++;
        return true;
    }

    public bool RemoveMember(string firstName, string lastName)
    {
        for (int i = 0; i < memberCount; i++)
        {
            if (members[i].FirstName == firstName && members[i].LastName == lastName)
            {
                if (members[i].BorrowedCount > 0)
                {
                    Console.WriteLine("Member cannot be removed. They must return all borrowed DVDs first.");
                    return false;
                }

                // Move the last member into the removed member's place
                members[i] = members[memberCount - 1];
                members[memberCount - 1] = null;
                memberCount--;
                return true;
            }
        }
        Console.WriteLine("Member not found.");
        return false;
    }

    public Member? FindMember(string firstName, string lastName)
    {
        for (int i = 0; i < memberCount; i++)
        {
            if (members[i].FirstName == firstName && members[i].LastName == lastName)
            {
                return members[i];
            }
        }
        return null;
    }

    public Member[] GetAllMembers()
    {
        Member[] allMembers = new Member[memberCount];
        Array.Copy(members, allMembers, memberCount);
        return allMembers;
    }

    private void ResizeArray()
    {
        Member[] newArray = new Member[members.Length * 2];
        Array.Copy(members, newArray, members.Length);
        members = newArray;
    }
}
