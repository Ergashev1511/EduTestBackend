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
    public class TestStatusUpdateHandler : IRequestHandler<TestStatusUpdateCommand, bool>
    {
        private readonly ITestRepository _repository;
        public TestStatusUpdateHandler(ITestRepository testRepository)
        {
            _repository = testRepository;
        }
        public async Task<bool> Handle(TestStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                    return false;


                await _repository.StatusUpdateAsyn(request.Id, request.testStatus);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
