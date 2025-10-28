using AutoFixture;
using BugStore.Application.Customers.Handlers;
using BugStore.Application.Customers.Requests;
using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace BugStore.Application.Tests.Customers.Handlers
{
    public class GetCustomersRequestHandlerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<ICustomerRepository> customerRepository;
        private readonly GetCustomersRequestHandler handler;

        public GetCustomersRequestHandlerTests()
        {
            fixture = new Fixture();
            customerRepository = new Mock<ICustomerRepository>();
            handler = new GetCustomersRequestHandler(customerRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmpty_WhenNoCustomerFound()
        {
            customerRepository.Setup(x => x.GetAllAsync(CancellationToken.None))
                .ReturnsAsync([]);

            var request = fixture.Create<GetCustomersRequest>();

            var result = await handler.Handle(request, CancellationToken.None);

            result.Customers.Should().NotBeNull();
            result.Customers.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_Should_ReturnResults_WhenSuccess()
        {
            var expectedCount = 3;
            var customer = fixture.CreateMany<Customer>(expectedCount);

            customerRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Customer, bool>>>(),CancellationToken.None))
                .ReturnsAsync(customer);

            var request = fixture.Create<GetCustomersRequest>();

            var result = await handler.Handle(request, CancellationToken.None);

            result.Customers.Should().NotBeNull();
            result.Customers.Should().HaveCount(expectedCount);
        }
    }
}
