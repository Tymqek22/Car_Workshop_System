using Car_Workshop_System.Application.DTO;
using Car_Workshop_System.Application.Interfaces;
using Car_Workshop_System.Application.Services;
using Car_Workshop_System.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Car_Workshop_System.Application.Tests.Services
{
	public class WorkOrderServiceTests
	{
		[Fact]
		public async Task AcceptWorkOrder_ShouldSaveOrder_WhenDataIsValid()
		{
			var workOrderRepoMock = new Mock<IWorkOrderRepository>();
			var technicianAssignmentRepoMock = new Mock<ITechnicianAssignmentRepository>();
			var identityServiceMock = new Mock<IIdentityService>();
			var valdatorResolverMock = new Mock<IValidatorResolver>();

			var validatorMock = new Mock<IValidator<AcceptWorkOrderDto>>();

			valdatorResolverMock
				.Setup(x => x.Get<AcceptWorkOrderDto>())
				.Returns(validatorMock.Object);

			validatorMock
				.Setup(x => x.ValidateAsync(It.IsAny<AcceptWorkOrderDto>(),default))
				.ReturnsAsync(new ValidationResult());

			var service = new WorkOrderService(
				workOrderRepoMock.Object,
				technicianAssignmentRepoMock.Object,
				identityServiceMock.Object,
				valdatorResolverMock.Object);

			var dto = new AcceptWorkOrderDto
			{
				Brand = "Audi",
				Model = "A4",
				Year = 2020,
				IssueDescription = "Wheel noise"
			};

			var result = await service.AcceptWorkOrder(dto);

			result.IsSuccess.Should().BeTrue();

			workOrderRepoMock.Verify(x => x.AddAsync(It.IsAny<WorkOrder>()),Times.Once());
			workOrderRepoMock.Verify(x => x.SaveChangesAsync(),Times.Once());
		}

		[Fact]
		public async Task AcceptWorkOrder_ShouldReturnFailure_WhenValidationFails()
		{
			var workOrderRepoMock = new Mock<IWorkOrderRepository>();
			var technicianAssignmentRepoMock = new Mock<ITechnicianAssignmentRepository>();
			var identityServiceMock = new Mock<IIdentityService>();
			var valdatorResolverMock = new Mock<IValidatorResolver>();

			var validatorMock = new Mock<IValidator<AcceptWorkOrderDto>>();

			var validationResult = new ValidationResult(new[]
			{
				new ValidationFailure("Brand", "Brand is required.")
			});

			valdatorResolverMock
				.Setup(x => x.Get<AcceptWorkOrderDto>())
				.Returns(validatorMock.Object);

			validatorMock
				.Setup(x => x.ValidateAsync(It.IsAny<AcceptWorkOrderDto>(),default))
				.ReturnsAsync(validationResult);

			var service = new WorkOrderService(
				workOrderRepoMock.Object,
				technicianAssignmentRepoMock.Object,
				identityServiceMock.Object,
				valdatorResolverMock.Object);

			var dto = new AcceptWorkOrderDto();

			var result = await service.AcceptWorkOrder(dto);

			result.IsSuccess.Should().BeFalse();

			workOrderRepoMock.Verify(x => x.AddAsync(It.IsAny<WorkOrder>()),Times.Never());
			workOrderRepoMock.Verify(x => x.SaveChangesAsync(),Times.Never());
		}
	}
}
