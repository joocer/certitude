namespace Divvy.Drone.Threads
{
    class ThreadManager
    {
        //private Consumer _consumer;

        public void Initialize()
        {
            //if (_consumer == null)
            //{
            //    //TODO: needs to be configured
            //    _consumer = new Consumer("192.168.1.14", "test.q");
            //}
        }

        public void Start()
        {
            //_consumer.StartConsuming();
            //_consumer.OnMessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(byte[] message)
        {
            string source = System.Text.Encoding.ASCII.GetString(message);
            TaskQueue.Enqueue(source);
        }

        public void Manage()
        {
        }

        public void Stop()
        {
            
        }
    }
}
