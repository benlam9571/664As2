public class MemberCollection
{
    private const int MaxSize = 1000;
    private Member[] members;
    private int memberCount;

    public MemberCollection()
    {
        members = new Member[MaxSize];
        memberCount = 0;
    }

    public bool AddMember(Member member)
    {
        if (memberCount >= MaxSize)
        {
            Console.WriteLine("Member collection is full.");
            return false;
        }

        for (int i = 0; i < memberCount; i++)
        {
            if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
            {
                Console.WriteLine("Member already exists.");
                return false;
            }
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
}
