﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Linq;
using Taarafo.Core.Models.PostReports;

namespace Taarafo.Core.Brokers.Storages
{
    public partial interface IStrorageBroker
    {
        IQueryable<PostReport> SelectAllPostReports();
        ValueTask<PostReport> SelectPostReportByIdAsync(Guid Id);
    }
}
