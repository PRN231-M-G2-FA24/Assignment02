using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class MemberRepository : IMemberRepository
    {
        public Task<Member> Authentication(string email, string password)
        {
            return MemberDAO.Instance.Authentication(email, password);
        }

        public void DeleteMember(Member p) => MemberDAO.DeleteMember(p);

        public List<Member> GetMember() => MemberDAO.GetMember();

        public Member GetMemberById(int id) => MemberDAO.FindMemberById(id);

        public void SaveMember(Member c) => MemberDAO.SaveMember(c);

        public void UpdateMember(Member p) => MemberDAO.UpdateMember(p);
    }
}
