namespace Car_Workshop_System.Application.Interfaces
{
	public interface IIdentityService
	{
		public Task<string> GetUserById(string id);
	}
}
