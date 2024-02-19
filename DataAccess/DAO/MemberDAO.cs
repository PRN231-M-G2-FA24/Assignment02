using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();

        private MemberDAO()
        {
        }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }

                    return instance;
                }
            }
        }
        public async Task<Member> Authentication(string email, string password)
        {
            var context = new AppDbContext();
            var list = context.Members.ToList();
            return list.Where(member =>
                    member.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && member.Password.Equals(password))
                .FirstOrDefault();
        }
        public static List<Member> GetMember()
        {
            var listMember = new List<Member>();

            try
            {

                using (var context = new AppDbContext())
                {
                    Console.WriteLine("check");
                    listMember = context.Members.ToList();

                    Console.WriteLine("end check");
                    foreach (var mem in listMember)
                    {
                        Console.WriteLine(mem);
                    }
                    Console.WriteLine(listMember.Count());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listMember;

        }
        public static Member FindMemberById(int MemberId)
        {
            Member p = new Member();
            try
            {
                using (var context = new AppDbContext())
                {
                    p = context.Members.SingleOrDefault(x => x.MemberId == MemberId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return p;
        }
        public static void SaveMember(Member p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Members.Add(p);
                    context.SaveChanges();
                }

                Console.WriteLine("check");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateMember(Member p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Members.Update(p);
                    // context.Entry<Products>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void DeleteMember(Member p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var p1 = context.Members.SingleOrDefault(x => x.MemberId == p.MemberId);
                    context.Members.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
