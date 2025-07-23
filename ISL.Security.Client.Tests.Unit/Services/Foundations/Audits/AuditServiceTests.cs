﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using ISL.Security.Client.Brokers.DateTimes;
using ISL.Security.Client.Services.Foundations.Audits;
using Moq;
using Tynamix.ObjectFiller;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditServiceTests
    {
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IAuditService auditService;

        public AuditServiceTests()
        {
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.auditService = new AuditService(dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}