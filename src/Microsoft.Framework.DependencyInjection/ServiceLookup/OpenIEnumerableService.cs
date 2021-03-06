// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;

namespace Microsoft.Framework.DependencyInjection.ServiceLookup
{
    internal class OpenIEnumerableService : IGenericService
    {
        private readonly ServiceTable _table;

        public OpenIEnumerableService(ServiceTable table)
        {
            _table = table;
        }

        public LifecycleKind Lifecycle
        {
            get { return LifecycleKind.Transient; }
        }

        public IService GetService(Type closedServiceType)
        {
            var itemType = closedServiceType.GetTypeInfo().GenericTypeArguments[0];

            ServiceEntry entry;
            return _table.TryGetEntry(itemType, out entry) ?
                new ClosedIEnumerableService(itemType, entry) :
                null;
        }
    }
}
