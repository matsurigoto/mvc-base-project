using System;

namespace Core.Common.Log
{
    /// <summary>
    /// Log功能的interface
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Traces 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Trace(string message);

        /// <summary>
        /// Traces 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Trace(object outputObject);

        /// <summary>
        /// Traces 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Traces 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Trace(string message, object outputObject);

        /// <summary>
        /// Debug 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Debug(string message);

        /// <summary>
        /// Debug 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Debug 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Debug(object outputObject);

        /// <summary>
        /// Debug 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Debug(string message, object outputObject);

        /// <summary>
        /// Info 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Info(string message);

        /// <summary>
        /// Info 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Info 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Info(object outputObject);

        /// <summary>
        /// Info 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Info(string message, object outputObject);

        /// <summary>
        /// Warn 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Warn(string message);

        /// <summary>
        /// Warn 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Warn 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Warn(object outputObject);

        /// <summary>
        /// Warn 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Warn(string message, object outputObject);

        /// <summary>
        /// Error 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Error(string message);

        /// <summary>
        /// Error 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Error 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Error(object outputObject);

        /// <summary>
        /// Error 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Error(string message, object outputObject);

        /// <summary>
        /// Fatal 訊息
        /// </summary>
        /// <param name="message">訊息</param>
        void Fatal(string message);

        /// <summary>
        /// Fatal 訊息加上format的參數
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="args">format的參數</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Fatal 把某個物件內容dump出來
        /// </summary>
        /// <param name="outputObject">要dump的物件</param>
        void Fatal(object outputObject);

        /// <summary>
        /// Fatal 把某個物件內容dump出來，並且在dump內容加上一段訊息
        /// </summary>
        /// <param name="message">加上的訊息</param>
        /// <param name="outputObject">要dump的物件</param>
        void Fatal(string message, object outputObject);

        /// <summary>
        /// Errors 記錄訊息和Exception
        /// </summary>
        /// <param name="message">要記錄的訊息</param>
        /// <param name="ex">要記錄的Exception</param>
        void Error(string message, Exception ex);
    }
}
