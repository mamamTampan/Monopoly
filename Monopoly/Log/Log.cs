using log4net;

namespace MonopolyLog
{
	public sealed class Log
	{
		private static Log? _instance;
		private static readonly object _lock = new object();
		private static readonly ILog logger = LogManager.GetLogger(typeof(Log));

		private Log() { }

		public static Log Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
						{
							_instance = new Log();
						}
					}
				}
				return _instance;
			}
		}

		public void Debug(string message)
		{
			logger.Debug(message);
		}

		public void Info(string message)
		{
			logger.Info(message);
		}   

		public void Warn(string message)
		{
			logger.Warn(message);
		}

		public void Error(string message)
		{
			logger.Error(message);
		}

		public void Fatal(string message)
		{
			logger.Fatal(message);
		}
	}
}
