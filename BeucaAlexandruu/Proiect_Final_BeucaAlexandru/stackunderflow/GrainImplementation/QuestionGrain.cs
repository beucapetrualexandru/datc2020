using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainImplementation
{
    public class QuestionGrain: Orleans.Grain
    {
        private StackUnderflowContext _dbContext;
        public QuestionGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }
    };
}
