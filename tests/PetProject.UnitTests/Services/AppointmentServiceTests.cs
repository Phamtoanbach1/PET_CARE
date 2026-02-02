using AutoMapper;
using Moq;
using PetProject.Application.DTOs;
using PetProject.Application.Services;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using Xunit;

namespace PetProject.UnitTests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRepository<Appointment>> _mockRepo;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<IRepository<Appointment>>();

            _mockUnitOfWork
                .Setup(u => u.Repository<Appointment>())
                .Returns(_mockRepo.Object);

            _service = new AppointmentService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var dto = new BookingCreateDto { PetId = 1, ServiceId = 2 };
            var userId = "user123";

            var mappedEntity = new Appointment { PetId = 1 };
            _mockMapper.Setup(m => m.Map<Appointment>(dto))
                       .Returns(mappedEntity);

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.CreateAppointmentAsync(dto, userId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.AddAsync(It.Is<Appointment>(
                a => a == mappedEntity && a.PetId == 1
            )), Times.Once);

            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldCallRepositoryAndUnitOfWork()
        {
            // Arrange
            var dto = new BookingCreateDto { PetId = 5, ServiceId = 10 };
            var userId = "user999";

            var mappedEntity = new Appointment();
            _mockMapper.Setup(m => m.Map<Appointment>(dto))
                       .Returns(mappedEntity);

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.CreateAppointmentAsync(dto, userId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Appointment>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldSetPetIdFromDto()
        {
            // Arrange
            var dto = new BookingCreateDto { PetId = 42, ServiceId = 2 };
            var userId = "user123";

            var mappedEntity = new Appointment();
            _mockMapper.Setup(m => m.Map<Appointment>(dto))
                       .Returns(mappedEntity);

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.CreateAppointmentAsync(dto, userId);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Appointment>(a => a.PetId == 42)), Times.Once);
        }
    }
}
