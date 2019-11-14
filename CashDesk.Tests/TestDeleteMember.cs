using System;
using System.Threading.Tasks;
using Xunit;

namespace CashDesk.Tests
{
    public class TestDeleteMember
    {
        [Fact]
        public void InvalidParameters()
        {
            using (var ab = new DataAccess())
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await ab.DeleteMemberAsync(Int32.MaxValue));
            }
        }

        [Fact]
        public async Task DeleteMember()
        {
            using (var ab = new DataAccess())
            {
                await ab.InitializeDatabaseAsync();
                var mnummer = await ab.AddMemberAsync("Foo", "DeleteMember", DateTime.Today.AddYears(-18));
                await ab.DeleteMemberAsync(mnummer);
            }
        }

        [Fact]
        public async Task CascadeDeleteMember()
        {
            using (var ab = new DataAccess())
            {
                await ab.InitializeDatabaseAsync();
                var mnummer = await ab.AddMemberAsync("Foo", "CascadeDeleteMember", DateTime.Today.AddYears(-18));
                await ab.JoinMemberAsync(mnummer);
                await ab.DepositAsync(mnummer, 100);
                await ab.DeleteMemberAsync(mnummer);
            }
        }
    }
}
