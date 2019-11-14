using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDesk
{
    /// <inheritdoc />
    public class DataAccess : IDataAccess
    {
        private CashDeskDataContext context;

        /// <inheritdoc />
        public Task InitializeDatabaseAsync()
        {
            if (context != null)
            {
                throw new InvalidOperationException();
            }
            context = new CashDeskDataContext();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task<int> AddMemberAsync(string firstName, string lastName, DateTime birthday)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || birthday == null)
            {
                throw new ArgumentException("Firstname, Lastname, or birthday is null or empty!");
            }
            if (context.Members.Where(curMember => curMember.LastName.Equals(lastName)).Count() > 0)
            {
                throw new DuplicateNameException("The lastname " + lastName + " already exists!");
            }
            Member member = new Member { FirstName = firstName, LastName = lastName, Birthday = birthday };
            context.Members.Add(member);
            await context.SaveChangesAsync();
            return member.MemberNumber;
        }

        /// <inheritdoc />
        public async Task DeleteMemberAsync(int memberNumber)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }

            try
            {
                var foundMember = context.Members.Single(member => member.MemberNumber == memberNumber);
                context.Members.Remove(foundMember);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }


        /// <inheritdoc />
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }

        public Task<IMembership> JoinMemberAsync(int memberNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IMembership> CancelMembershipAsync(int memberNumber)
        {
            throw new NotImplementedException();
        }

        public Task DepositAsync(int memberNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDepositStatistics>> GetDepositStatisticsAsync()
        {
            throw new NotImplementedException();
        }
    }
}