using System;
using System.Collections.Generic;
using System.Linq;
using PittsburghBabaTemple.Core.Interfaces;
using NHibernate;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace PittsburghBabaTemple.Core.Data
{
    public abstract class ActiveRecordBase<T> where T : class, new()
    {
        private static ISession Session { get; set; }
        private static readonly object Padlock = new Object();

        public static T Find(int id) 
        {
            lock (Padlock)
            {
                T t = new T();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    var entity = Session.Get<T>(id);
                    if (entity != null) t = entity;
                }
                return t;
            }
        }

        public static T Load(int id)
        {
            lock (Padlock)
            {
                T t = new T();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    var entity = Session.Load<T>(id);
                    if (entity != null) t = entity;
                }
                return t;
            }
        }

        public static IList<T> Find(IList<int> ids) 
        {
            lock (Padlock)
            {
                IList<T> list = new List<T>();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    list = Session.CreateCriteria<T>()
                        .Add(Restrictions.In("Id", ids.ToArray()))
                        .List<T>();
                }
                return list;
            }
        }

        public static IList<T> Find(IList<int> ids, IFetchMode mode)
        {
            lock (Padlock)
            {
                IList<T> list = new List<T>();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    list = mode.Fetch(Session.CreateCriteria<T>())
                        .Add(Restrictions.In("Id", ids.ToArray()))
                        .List<T>();
                }
                return list;
            }
        }

        public static IList<T> Find(Expression<Func<T, bool>> where)
        {
            lock (Padlock)
            {
                IList<T> list = new List<T>();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    list = Session.Query<T>().Where(where).ToList();
                }
                return list;
            }
        }

        public static IList<T> Find(Expression<Func<T, bool>> where, IFetchingStrategy<T> strategy)
        {
            lock (Padlock)
            {
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    return strategy.Fetch(Session.Query<T>().Where(where)).ToList();
                }
            }
        }

        public static T FindOne(Expression<Func<T, bool>> where)
        {
            lock (Padlock)
            {
                T t = new T();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    var entity = Session.Query<T>().Where(where).FirstOrDefault();
                    if (entity != null) t = entity;
                }
                return t;
            }
        }

        public static T FindOne(Expression<Func<T, bool>> where, IFetchingStrategy<T> strategy)
        {
            lock (Padlock)
            {
                T t = new T();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    var entity = strategy.Fetch(Session.Query<T>().Where(where)).FirstOrDefault();
                    if (entity != null) t = entity;
                }
                return t;
            }
        }

        public static IList<T> FindAll()
        {
            lock (Padlock)
            {
                IList<T> list = new List<T>();
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    list = Session.QueryOver<T>().List<T>();
                }
                return list;
            }
        }

        public static int Count()
        {
            lock (Padlock)
            {
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    return Session.Query<T>().Count();
                }
            }
        }

        public static int Count(Expression<Func<T, bool>> where)
        {
            lock (Padlock)
            {
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    return Session.Query<T>().Count(where);
                }
            }
        }

        public static IList<T> FindAll(IFetchingStrategy<T> strategy)
        {
            lock (Padlock)
            {
                Session = SessionConfiguration.GetCurrentSession();
                using (Session.BeginTransaction())
                {
                    return strategy.Fetch(Session.Query<T>()).ToList();
                }
            }
        }

        public virtual void Delete()
        {
            Session = SessionConfiguration.GetCurrentSession();
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(this);
                transaction.Commit();
            }
        }

        public virtual void Save()
        {
            Session = SessionConfiguration.GetCurrentSession();
            using (var transaction = Session.BeginTransaction())
            {
                var t = Session.Merge(this) as IEntity;
                transaction.Commit();
                if (t != null) ((IEntity)this).Id = t.Id;
            }
        }
    }
}