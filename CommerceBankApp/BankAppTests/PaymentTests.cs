using CommerceBankApp.Areas.Identity.Data;
using CommerceBankApp.Controllers;
using CommerceBankApp.Data;
using CommerceBankApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;

namespace BankAppTesting.Moq
{
    public class PaymentControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly PaymentController _controller;

        public PaymentControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var _context = new ApplicationDbContext(options);
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _controller = new PaymentController(_context, _userManager.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        private Payment GetPayment(int id)
        {
            var payment = new Payment()
            {
                PaymentId = id,
                DonatedAmount = 100,
                DonatedDate = System.DateTime.Now,
            };
            return payment;
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Details_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_IDisFound_ReturnsViewResult()
        {
            await _controller.Create(GetPayment(1));
            var result = await _controller.Details(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var org = new Organization()
            {
                OrganizationID = 1,
                OrganizationName = "Hello",
                OrganizationDescription = "This is a test.",
                ImageUrl = "https://media.istockphoto.com/id/1270939459/vector/fundraising-round-ribbon-isolated-label-fundraising-sign.jpg?s=612x612&w=0&k=20&c=uUGQb0L8AdaHHR7pjk_kYWd587mnGv3gXc5OLHTK3Gk="

            };
            var httpContext = new DefaultHttpContext();
            _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData["OrganizationName"] = org.OrganizationName;
            _controller.TempData["OrganizationImage"] = org.ImageUrl;
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ModelStateisValid_ReturnsRedirectResultForCreate()
        {
            var result = await _controller.Create(GetPayment(1));
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task CreateAsync_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("DonatedAmount", "Amount is required");
            var payment = new Payment
            {
                PaymentId = 1,
                DonatedDate = System.DateTime.Now,
            };
            var result = await _controller.Create(payment);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testPay = Assert.IsType<Payment>(viewResult.Model);

            Assert.Equal(payment.PaymentId, testPay.PaymentId);
        }

        [Fact]
        public async Task Edit_ActionExecutes_ReturnsViewResultForEdit()
        {
            await _controller.Create(GetPayment(1));
            var result = await _controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ActionExecutes_ReturnsRedirectResultForEdit()
        {
            var payment = GetPayment(1);
            await _controller.Create(payment);
            var result = await _controller.Edit(1, payment);
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task EditFull_IDisInvalid_ReturnsNotFoundResult()
        {
            var payment = GetPayment(1);
            await _controller.Create(payment);
            var result = await _controller.Edit(2, payment);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ModelisInvalid_ReturnsViewResultForEdit()
        {
            _controller.ModelState.AddModelError("OrganizationName", "Name is required");
            var payment = new Payment
            {
            

            };
            var result = await _controller.Edit(0, payment);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ActionExecutes_ReturnsViewResultForDelete()
        {
            var payment = GetPayment(1);
            await _controller.Create(payment);
            var result = await _controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ActionExecutes_ReturnsRedirectResultForDeleteConfirmed()
        {
            var payment = GetPayment(1);
            await _controller.Create(payment);
            var result = await _controller.DeleteConfirmed(1);
            Assert.IsType<RedirectResult>(result);
        }
    }
}
