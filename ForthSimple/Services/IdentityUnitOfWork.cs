using Forth.Identity.Entities;
using ForthSimple.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace ForthSimple.Services
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        public SignInManager<User> SignInManager { get; set; }

        public UserManager<User> UserManager { get; set; }

        public IdentityUnitOfWork(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                }
                disposed = true;
            }
        }

        ~IdentityUnitOfWork()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}