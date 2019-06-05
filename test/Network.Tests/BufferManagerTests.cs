using Imgeneus.Network.Common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Xunit;

namespace Network.Tests
{
    public class BufferManagerTests : IDisposable
    {
        private readonly int numberOfSlots = 50;
        private readonly int bufferSize;
        private readonly int totalBufferSize;
        private readonly BufferManager bufferManager;

        public BufferManagerTests()
        {
            this.bufferSize = 256;
            this.totalBufferSize = this.bufferSize * this.numberOfSlots;
            this.bufferManager = new BufferManager(this.numberOfSlots, this.bufferSize);
        }

        [Fact]
        public void CreateBufferManagerTest()
        {
            Assert.NotNull(this.bufferManager);
            Assert.Equal(this.totalBufferSize, this.bufferManager.Size);
        }

        [Fact]
        public void GetBufferTest()
        {
            var socketAsync = new SocketAsyncEventArgs();

            Assert.Null(socketAsync.Buffer);
            bool setBufferResult = this.bufferManager.SetBuffer(socketAsync);

            Assert.True(setBufferResult);
            Assert.NotNull(socketAsync.Buffer);
            Assert.Equal(this.totalBufferSize, socketAsync.Buffer.Length);
            Assert.Equal(0, socketAsync.Offset);
            Assert.Equal(this.bufferSize, socketAsync.Count);
        }

        [Fact]
        public void FreeBufferTest()
        {
            var socketAsync = new SocketAsyncEventArgs();

            Assert.Null(socketAsync.Buffer);
            bool setBufferResult = this.bufferManager.SetBuffer(socketAsync);

            Assert.True(setBufferResult);
            Assert.NotNull(socketAsync.Buffer);
            Assert.Equal(this.totalBufferSize, socketAsync.Buffer.Length);
            Assert.Equal(0, socketAsync.Offset);
            Assert.Equal(this.bufferSize, socketAsync.Count);

            this.bufferManager.FreeBuffer(socketAsync);
            Assert.Null(socketAsync.Buffer);
            Assert.Equal(0, socketAsync.Offset);
            Assert.Equal(0, socketAsync.Count);
        }

        [Fact]
        public void DisposeBufferManagerTest()
        {
            Assert.Equal(this.totalBufferSize, this.bufferManager.Size);
            this.bufferManager.Dispose();
        }

        [Fact]
        public void GetSetMultipleBuffersTest()
        {
            const int number = 20;
            var socketsEvents = new SocketAsyncEventArgs[number];

            for (int i = 0; i < number; i++)
            {
                socketsEvents[i] = new SocketAsyncEventArgs();
                var socketAsync = socketsEvents[i];

                Assert.Null(socketAsync.Buffer);

                bool setBufferResult = this.bufferManager.SetBuffer(socketAsync);

                Assert.True(setBufferResult);
                Assert.NotNull(socketAsync.Buffer);
                Assert.Equal(this.bufferSize * i, socketAsync.Offset);
            }

            for (int i = 0; i < 10; i++)
            {
                this.bufferManager.FreeBuffer(socketsEvents[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                var socketAsync = socketsEvents[i];
                Assert.Null(socketAsync.Buffer);

                bool setBufferResult = this.bufferManager.SetBuffer(socketAsync);

                Assert.True(setBufferResult);
                Assert.NotNull(socketAsync.Buffer);
            }
        }

        [Fact]
        public void SetBufferWhenNoMoreSpaceAvailableTest()
        {
            var socketsEvents = new SocketAsyncEventArgs[this.numberOfSlots];

            for (int i = 0; i < this.numberOfSlots; i++)
            {
                socketsEvents[i] = new SocketAsyncEventArgs();
                var socketAsync = socketsEvents[i];

                Assert.Null(socketAsync.Buffer);

                bool setBufferResult = this.bufferManager.SetBuffer(socketAsync);

                Assert.True(setBufferResult);
                Assert.NotNull(socketAsync.Buffer);
                Assert.Equal(this.bufferSize * i, socketAsync.Offset);
            }

            var otherSocketAsync = new SocketAsyncEventArgs();
            bool result = this.bufferManager.SetBuffer(otherSocketAsync);

            Assert.False(result);
        }

        public void Dispose() => this.bufferManager.Dispose();
    }
}
