#region Copyright notice and license

// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Threading;
using System.Threading.Tasks;
using Grpc.Core.Utils;

namespace Grpc.Core;

/// <summary>
/// Asynchronous authentication interceptor for <see cref="CallCredentials"/>.
/// </summary>
/// <param name="context">The interceptor context.</param>
/// <param name="metadata">Metadata to populate with entries that will be added to outgoing call's headers.</param>
/// <returns></returns>
public delegate Task AsyncAuthInterceptor(AuthInterceptorContext context, Metadata metadata);

/// <summary>
/// Context for an RPC being intercepted by <see cref="AsyncAuthInterceptor"/>.
/// </summary>
public class AuthInterceptorContext
{
    private readonly string serviceUrl;
    private readonly string methodName;
    private readonly CancellationToken cancellationToken;

    /// <summary>
    /// Initializes a new instance of <c>AuthInterceptorContext</c>.
    /// </summary>
    public AuthInterceptorContext(string serviceUrl, string methodName) : this(serviceUrl, methodName, CancellationToken.None)
    {
    }

    /// <summary>
    /// Initializes a new instance of <c>AuthInterceptorContext</c>.
    /// </summary>
    public AuthInterceptorContext(string serviceUrl, string methodName, CancellationToken cancellationToken)
    {
        this.serviceUrl = GrpcPreconditions.CheckNotNull(serviceUrl, nameof(serviceUrl));
        this.methodName = GrpcPreconditions.CheckNotNull(methodName, nameof(methodName));
        this.cancellationToken = cancellationToken;
    }

    /// <summary>
    /// The fully qualified service URL for the RPC being called.
    /// </summary>
    public string ServiceUrl => serviceUrl;

    /// <summary>
    /// The method name of the RPC being called.
    /// </summary>
    public string MethodName => methodName;

    /// <summary>
    /// The cancellation token of the RPC being called.
    /// </summary>
    public CancellationToken CancellationToken => cancellationToken;
}
