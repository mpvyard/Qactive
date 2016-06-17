﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Qactive.Tests.Tcp.Operators
{
  [TestClass]
  public class GroupJoinTests : TestBase
  {
    [TestMethod]
    public async Task GroupJoin()
    {
      var service = TcpTestService.Create(TcpTestService.UnrestrictedOptions, Observable.Range(0, 6));

      var results = await service.QueryAsync(source => from x in source
                                                       join y in Observable.Range(3, 5)
                                                       on Observable.Never<Unit>() equals Observable.Never<Unit>()
                                                       into ys
                                                       from y in ys.Take(3)
                                                       where x == y
                                                       select x + y);

      // TODO: The second parameter of Qbservable.Join is an IObservable<T> rather than an IQbservable<T>. Consider exposing various built-in serializable observable generators.
      QactiveAssert.AreEqual(results, OnError<int>(new SerializationException("Type 'System.Reactive.Linq.ObservableImpl.Range' in Assembly 'System.Reactive.Linq, Version=2.2.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' is not marked as serializable.")));
      //QactiveAssert.Equal(results, OnNext(6), OnNext(8), OnNext(10), OnCompleted<int>());
    }

    [TestMethod]
    public async Task GroupJoinClosure()
    {
      var service = TcpTestService.Create(TcpTestService.UnrestrictedOptions, Observable.Range(0, 6));
      var range3To7 = Observable.Range(3, 5);

      var results = await service.QueryAsync(source => from x in source
                                                       join y in range3To7
                                                       on Observable.Never<Unit>() equals Observable.Never<Unit>()
                                                       into ys
                                                       from y in ys.Take(3)
                                                       where x == y
                                                       select x + y);

      // TODO: The second parameter of Qbservable.Join is an IObservable<T> rather than an IQbservable<T>. Consider exposing various built-in serializable observable generators.
      QactiveAssert.AreEqual(results, OnError<int>(new SerializationException("Type 'System.Reactive.Linq.ObservableImpl.Range' in Assembly 'System.Reactive.Linq, Version=2.2.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' is not marked as serializable.")));
      //QactiveAssert.Equal(results, OnNext(6), OnNext(8), OnNext(10), OnCompleted<int>());
    }

    [TestMethod]
    public async Task GroupJoinDurationClosure()
    {
      var service = TcpTestService.Create(TcpTestService.UnrestrictedOptions, Observable.Range(0, 6));
      var range3To7 = Observable.Range(3, 5);
      var otherDuration = Observable.Never<Unit>();

      var results = await service.QueryAsync(source => from x in source
                                                       join y in range3To7
                                                       on Observable.Never<Unit>() equals otherDuration
                                                       into ys
                                                       from y in ys.Take(3)
                                                       where x == y
                                                       select x + y);

      // TODO: The second parameter of Qbservable.Join is an IObservable<T> rather than an IQbservable<T>. Consider exposing various built-in serializable observable generators.
      QactiveAssert.AreEqual(results, OnError<int>(new SerializationException("Type 'System.Reactive.Linq.ObservableImpl.Range' in Assembly 'System.Reactive.Linq, Version=2.2.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' is not marked as serializable.")));
      //QactiveAssert.Equal(results, OnNext(6), OnNext(8), OnNext(10), OnCompleted<int>());
    }

    [TestMethod]
    public async Task GroupJoinWithContext()
    {
      var service = TcpTestService.Create(TcpTestService.UnrestrictedOptions, Observable.Return(new TestContext()));

      var results = await service.QueryAsync(source => from context in source
                                                       from x in context.Range0To5
                                                       join y in Observable.Range(3, 5)
                                                       on Observable.Never<Unit>() equals Observable.Never<Unit>()
                                                       into ys
                                                       from y in ys.Take(3)
                                                       where x == y
                                                       select x + y);

      // TODO: The second parameter of Qbservable.Join is an IObservable<T> rather than an IQbservable<T>. Consider exposing various built-in serializable observable generators.
      QactiveAssert.AreEqual(results, OnError<int>(new SerializationException("Type 'System.Reactive.Linq.ObservableImpl.Range' in Assembly 'System.Reactive.Linq, Version=2.2.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' is not marked as serializable.")));
      //QactiveAssert.Equal(results, OnNext(6), OnNext(8), OnNext(10), OnCompleted<int>());
    }

    [TestMethod]
    public async Task GroupJoinClosureWithContext()
    {
      var service = TcpTestService.Create(TcpTestService.UnrestrictedOptions, Observable.Return(new TestContext()));
      var range3To7 = Observable.Range(3, 5);

      var results = await service.QueryAsync(source => from context in source
                                                       from x in context.Range0To5
                                                       join y in range3To7
                                                       on Observable.Never<Unit>() equals Observable.Never<Unit>()
                                                       into ys
                                                       from y in ys.Take(3)
                                                       where x == y
                                                       select x + y);

      // TODO: The second parameter of Qbservable.Join is an IObservable<T> rather than an IQbservable<T>. Consider exposing various built-in serializable observable generators.
      QactiveAssert.AreEqual(results, OnError<int>(new SerializationException("Type 'System.Reactive.Linq.ObservableImpl.Range' in Assembly 'System.Reactive.Linq, Version=2.2.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' is not marked as serializable.")));
      //QactiveAssert.Equal(results, OnNext(6), OnNext(8), OnNext(10), OnCompleted<int>());
    }

    private sealed class TestContext
    {
      public IObservable<int> Range0To5 => Observable.Range(0, 6);
    }
  }
}
