﻿using System;
using System.Threading;

namespace DAL
{
    /// <summary>
    /// 简单混合线程同步锁
    /// </summary>
    public sealed class SimpleHybirdLock : IDisposable
    {
        #region IDisposable Support

        //检测冗余调用
        private bool disposedValue = false;

        //实现可处置模式
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                m_waiterLock.Close();

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SimpleHybirdLock() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        #endregion IDisposable Support

        /// <summary>
        /// 基元用户模式构造同步锁
        /// </summary>
        private Int32 m_waiters = 0;

        /// <summary>
        /// 基元内核模式构造同步锁
        /// </summary>
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

        /// <summary>
        /// 获取当前锁是否在等待中
        /// </summary>
        public bool IsWaitting => m_waiters != 0;

        /// <summary>
        /// 获取锁
        /// </summary>
        public void Enter()
        {
            //用户锁可以使用的时候，直接返回，第一次调用时发生
            if (Interlocked.Increment(ref m_waiters) == 1) return;
            //当发生锁竞争时，使用内核同步构造锁
            m_waiterLock.WaitOne();
        }

        /// <summary>
        /// 离开锁
        /// </summary>
        public void Leave()
        {
            if (Interlocked.Decrement(ref m_waiters) == 0) return;//没有可用的锁的时候
            m_waiterLock.Set();
        }
    }
}