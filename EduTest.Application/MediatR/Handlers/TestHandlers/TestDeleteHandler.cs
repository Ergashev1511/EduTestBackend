using EduTest.Application.MediatR.Commands.TestCommands;
using EduTest.DataAccess.Repositories.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TestHandlers
{
    public class TestDeleteHandler : IRequestHandler<TestDeleteCommmand, bool>
    {
        private readonly ITestRepository _testRepository;
        public TestDeleteHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<bool> Handle(TestDeleteCommmand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Id <= 0)
                    return false;

                await _testRepository.DeleteAsync(request.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
