namespace Car_Workshop_System.Domain.Errors
{
	public record Error(string Code, string Description);

	public static class WorkOrderErrors
	{
		public static readonly Error OrderNotFound =
			new("WorkOrderErrors.OrderNotFound","Work order doesn't exist.");

		public static readonly Error TechnicianNotFound =
			new("WorkOrderErrors.TechnicianNotFound","Technician doesn't exist.");

		public static readonly Error TechnicianAlreadyAssigned =
			new("WorkOrderErrors.TechnicnianAlreadyAssigned","This technician has been already assigned to this order.");

		public static readonly Error AlreadyCancelled =
			new("WorkOrderErrors.AlreadyCancelled","Work order is already cancelled.");

		public static readonly Error WrongStatusPicked =
			new("WorkOrderErrors.WrongStatusPicked","Status cannot be changed. Try to pick different status.");
	}

	public static class AuthErrors
	{
		public static readonly Error UserNotFound =
			new("AuthErrors.UserNotFound","User doesn't exist.");

		public static readonly Error UserNotCreated =
			new("AuthErrors.UserNotCreated","User cannot be created.");

		public static readonly Error RoleAssignmentError =
			new("AuthErrors.RoleAssignmentError","User cannot be assigned to this role.");\

		public static readonly Error WrongPassword =
			new("AuthErrors.WrongPassword","Wrong password was typed.");
	}
}
