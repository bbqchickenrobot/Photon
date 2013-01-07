
using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace Photon.Web.Services
{
	public abstract class PhotonServiceBase<T>
	{
		protected IDocumentSession Session {get; set;}
		
	
		
		public PhotonServiceBase (IDocumentSession session)
		{
			this.Session = session;
		}
		
		protected virtual IRavenQueryable<T> All()
		{
			return this.Session.Query<T>();
		}
		
		public virtual T Load(string id)
		{
			return this.Session.Load<T>(id);
		}
		
		public virtual T Save(T entity)
		{
			this.Session.Store(entity);
			this.Session.SaveChanges();
			return entity;
		}
		
		public virtual void Delete(T entity)
		{
			this.Session.Delete(entity);
			this.Session.SaveChanges();
		}
	}
}
