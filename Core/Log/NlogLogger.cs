using System;
using Core.Common.Log;
using Newtonsoft.Json;
using NLog;

namespace Core.Log
{
    /// <summary>
    /// 使用Nlog作為ILog的實作
    /// </summary>
    public class NlogLogger : ILog
    {
        private Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NlogLogger"/> class.
        /// </summary>
        public NlogLogger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NlogLogger"/> class.
        /// </summary>
        /// <param name="name">目前要使用Log的Class名字</param>
        public NlogLogger(string name)
        {
            logger = NLog.LogManager.GetLogger(name);
        }

        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Trace(string message)
        {
            if (logger.IsTraceEnabled)
            {
                logger.Trace(message);
            }
        }

        /// <summary>
        /// Debug 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Debug(string message)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// Info 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Info(string message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// Warn 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Warn(string message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// Error 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Error(string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// Errors 記錄訊息和Exception
        /// </summary>
        /// <param name="message">要記錄的訊息</param>
        /// <param name="ex">要記錄的Exception</param>
        public void Error(string message, Exception ex)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, ex);
            }
        }

        /// <summary>
        /// Fatal 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        public void Fatal(string message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Trace(string message, params object[] args)
        {
            Trace(string.Format(message, args));
        }

        /// <summary>
        /// Debug 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Debug(string message, params object[] args)
        {
            Debug(string.Format(message, args));
        }

        /// <summary>
        /// Info 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Info(string message, params object[] args)
        {
            Info(string.Format(message, args));
        }

        /// <summary>
        /// Warn 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Warn(string message, params object[] args)
        {
            Warn(string.Format(message, args));
        }

        /// <summary>
        /// Error 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Error(string message, params object[] args)
        {
            Error(string.Format(message, args));
        }

        /// <summary>
        /// Fatal 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        public void Fatal(string message, params object[] args)
        {
            Fatal(string.Format(message, args));
        }

        /// <summary>
        /// Traces 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Trace(object outputObject)
        {
            Trace(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Debug 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Debug(object outputObject)
        {
            Debug(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Info 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Info(object outputObject)
        {
            Info(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Warn 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Warn(object outputObject)
        {
            Warn(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Error 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Error(object outputObject)
        {
            Error(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Fatal 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        public void Fatal(object outputObject)
        {
            Fatal(JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Traces 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Trace(string message, object outputObject)
        {
            Trace(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Debug 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Debug(string message, object outputObject)
        {
            Debug(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Info 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Info(string message, object outputObject)
        {
            Info(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Warn 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Warn(string message, object outputObject)
        {
            Warn(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Error 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Error(string message, object outputObject)
        {
            Error(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }

        /// <summary>
        /// Fatal 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        public void Fatal(string message, object outputObject)
        {
            Fatal(message + Environment.NewLine + JsonConvert.SerializeObject(outputObject, Formatting.Indented));
        }
    }
}
