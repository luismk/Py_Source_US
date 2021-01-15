using System;
namespace PangyaAPI.Helper
{
    public interface IDisposeable : IDisposable
    {
        bool Disposed { get; set; }
    }
}
