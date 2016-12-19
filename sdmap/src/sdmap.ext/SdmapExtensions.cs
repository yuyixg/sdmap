﻿using Dapper;
using sdmap.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sdmap.Extensions
{
    public static partial class SdmapExtensions
    {
        public static SdmapRuntime Runtime { get; set; }

        private static FileSystemWatcher watcher = null;

        public static void ResetSqlDirectory(string sqlDirectory)
        {
            if (watcher != null)
            {
                watcher.Dispose();
            }

            var runtime = new SdmapRuntime();

            foreach (var file in Directory.EnumerateFiles(sqlDirectory, "*.sdmap", SearchOption.AllDirectories))
            {
                var code = File.ReadAllText(file);
                runtime.AddSourceCode(code);
            }

            Runtime = runtime;
        }

        public static void ResetSqlDirectoryAndWatch(string sqlDirectory)
        {
            var runtime = new SdmapRuntime();

            if (watcher != null)
            {
                watcher.Dispose();
            }
            watcher = new FileSystemWatcher(sqlDirectory);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += (o, e) =>
            {
                GC.KeepAlive(watcher);
                watcher.Dispose();
                Thread.Sleep(1);
                ResetSqlDirectoryAndWatch(sqlDirectory);
            };

            foreach (var file in Directory.EnumerateFiles(sqlDirectory, "*.sdmap", SearchOption.AllDirectories))
            {
                var code = File.ReadAllText(file);
                runtime.AddSourceCode(code);
            }
            Runtime = runtime;
        }

        public static string EmitSql(string id, object param)
        {
            return Runtime.Emit(id, param);
        }

        public static int ExecuteById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public static IDataReader ExecuteReaderById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        public static object ExecuteScalarById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        public static T ExecuteScalarById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static IEnumerable<dynamic> QueryById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<T> QueryById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TThird, TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TThird, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null
        )
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TThird, TFourth,
            TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null
        )
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TThird, TFourth,
            TFifth, TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TThird, TFourth,
            TFifth, TSixth, TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> QueryById<TFirst, TSecond, TThird, TFourth,
            TFifth, TSixth, TSeventh, TReturn>(
            this IDbConnection cnn,
            string sqlId,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static dynamic QueryFirstById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirst(sql, param, transaction, commandTimeout, commandType);
        }

        public static object QueryFirstById(this IDbConnection cnn,
            Type type,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirst(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static T QueryFirstById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QueryFirstOrDefaultById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        public static object QueryFirstOrDefaultById(this IDbConnection cnn,
            Type type,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirstOrDefault(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static T QueryFirstOrDefaultById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QuerySingleById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingle(sql, param, transaction, commandTimeout, commandType);
        }

        public static object QuerySingleById(this IDbConnection cnn,
            Type type,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingle(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static T QuerySingleById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingle<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static dynamic QuerySingleOrDefaultById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingleOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        public static object QuerySingleOrDefaultById(
            this IDbConnection cnn,
            Type type,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingleOrDefault(type, sql, param, transaction, commandTimeout, commandType);
        }

        public static T QuerySingleOrDefaultById<T>(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static SqlMapper.GridReader QueryMultipleById(this IDbConnection cnn,
            string sqlId,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            var sql = EmitSql(sqlId, param);
            return cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
