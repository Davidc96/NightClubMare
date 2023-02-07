using CDJNFSLibrary.Protocols.Commons;
using CDJNFSLibrary.Protocols.V2;
using CDJNFSLibrary.Protocols.V3;
using CDJNFSLibrary.Protocols.V4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.String;

namespace CDJNFSLibrary
{
    /// <summary>
    /// NFS Client Library
    /// </summary>
    public class NfsClient
    {
        #region Enum

        /// <summary>
        /// The NFS version to use
        /// </summary>
        public enum NfsVersion
        {
            /// <summary>
            /// NFS Version 2
            /// </summary>
            V2 = 2,

            /// <summary>
            /// NFS Version 3
            /// </summary>
            V3 = 3,

            /// <summary>
            /// NFS Version 4.1
            /// </summary>
            V4 = 4
        }

        #endregion Enum

        #region Fields

        private NFSPermission _mode;
        private bool _isMounted;
        private bool _isConnected;
        private readonly string _currentDirectory = Empty;

        private readonly INFS _nfsInterface;

        /* Block size must not be greater than 8064 for V2 and
         * 8000 for V3. RPC Buffer size is fixed to 8192, when
         * requesting on RPC, 8192 bytes contain every details
         * of request. we reserve 128 bytes for header information
         * of V2 and 192 bytes for header information of V3.
         * V2: 8064 bytes for data.
         * V3: 8000 bytes for data. */
        public int blockSize = 7900;
        //this can change

        #endregion Fields

        #region Events

        public delegate void NfsDataEventHandler(object sender, NfsEventArgs e);

        /// <summary>
        /// This event is fired when data is transferred from/to the server
        /// </summary>
        public event NfsDataEventHandler DataEvent;

        public class NfsEventArgs : EventArgs
        {
            public NfsEventArgs(int bytes)
            {
                Bytes = bytes;
            }

            public int Bytes { get; }
        }

        #endregion Events

        #region Properties

        /// <summary>
        /// This property tells if the current export is mounted
        /// </summary>
        public bool IsMounted => _isMounted;

        /// <summary>
        /// This property tells if the connection is active
        /// </summary>
        public bool IsConnected => _isConnected;

        /// <summary>
        /// This property allow you to set file/folder access permissions
        /// </summary>
        public NFSPermission Mode
        {
            get => _mode ??= new NFSPermission(7, 7, 7);
            set => _mode = value;
        }

        /// <summary>
        /// This property contains the current server directory
        /// </summary>
        public string CurrentDirectory => _currentDirectory;

        #endregion Properties

        #region Constructor

        /// <summary>
        /// NFS Client Constructor
        /// </summary>
        /// <param name="version">The required NFS version</param>
        public NfsClient(NfsVersion version)
        {
            switch (version)
            {
                case NfsVersion.V2:
                    _nfsInterface = new NFSv2();
                    break;

                case NfsVersion.V3:
                    _nfsInterface = new NFSv3();
                    break;

                case NfsVersion.V4:
                    _nfsInterface = new NFSv4();
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Create a connection to a NFS Server
        /// </summary>
        /// <param name="address">The server address</param>
        public void Connect(IPAddress address)
        {
            Connect(address, 0, 0, 60000, System.Text.Encoding.ASCII, true, false);
        }

        /// <summary>
        /// Create a connection to a NFS Server
        /// </summary>
        /// <param name="address">The server address</param>
        /// <param name="userId">The unix user id</param>
        /// <param name="groupId">The unix group id</param>
        /// <param name="commandTimeout">The command timeout in milliseconds</param>
        public void Connect(IPAddress address, int userId, int groupId, int commandTimeout)
        {
            Connect(address, userId, groupId, commandTimeout, System.Text.Encoding.ASCII, true, false);
        }

        /// <summary>
        /// Create a connection to a NFS Server
        /// </summary>
        /// <param name="address">The server address</param>
        /// <param name="userId">The unix user id</param>
        /// <param name="groupId">The unix group id</param>
        /// <param name="commandTimeout">The command timeout in milliseconds</param>
        /// <param name="characterEncoding">Connection encoding</param>
        /// <param name="useSecurePort">Uses a local binding port less than 1024</param>
        public void Connect(IPAddress address, int userId, int groupId, int commandTimeout, System.Text.Encoding characterEncoding, bool useSecurePort, bool useCache)
        {
            _nfsInterface.Connect(address, userId, groupId, commandTimeout, characterEncoding, useSecurePort, useCache);
            _isConnected = true;
        }

        /// <summary>
        /// Close the current connection
        /// </summary>
        public void Disconnect()
        {
            _nfsInterface.Disconnect();
            _isConnected = false;
        }

        /// <summary>
        /// Get the list of the exported NFS devices
        /// </summary>
        /// <returns>A list of the exported NFS devices</returns>
        public List<string> GetExportedDevices()
        {
            return _nfsInterface.GetExportedDevices();
        }

        /// <summary>
        /// Mount device
        /// </summary>
        /// <param name="deviceName">The device name</param>
        public void MountDevice(string deviceName)
        {
            _nfsInterface.MountDevice(deviceName);
            //cuz of NFS v4.1 we have to do this after session is created
            blockSize = _nfsInterface.GetBlockSize();
            _isMounted = true;
        }

        /// <summary>
        /// Unmount the current device
        /// </summary>
        public void UnMountDevice()
        {
            _nfsInterface.UnMountDevice();
            _isMounted = false;
        }

        /// <summary>
        /// Get the items in a directory
        /// </summary>
        /// <param name="directoryFullName">Directory name (e.g. "directory\subdirectory" or "." for the root)</param>
        /// <returns>A list of the items name</returns>
        public List<string> GetItemList(string directoryFullName)
        {
            return GetItemList(directoryFullName, true);  //changed to true cuz i don't need .. and .
        }

        /// <summary>
        /// Get the items in a directory
        /// </summary>
        /// <param name="directoryFullName">Directory name (e.g. "directory\subdirectory" or "." for the root)</param>
        /// <param name="excludeNavigationDots">When posted as true, return list will not contains "." and ".."</param>
        /// <returns>A list of the items name</returns>
        public List<string> GetItemList(string directoryFullName, bool excludeNavigationDots)
        {
            directoryFullName = CorrectPath(directoryFullName);

            List<string> content = _nfsInterface.GetItemList(directoryFullName);

            if (excludeNavigationDots)
            {
                int dotIdx = content.IndexOf(".");
                if (dotIdx > -1)
                    content.RemoveAt(dotIdx);

                int ddotIdx = content.IndexOf("..");
                if (ddotIdx > -1)
                    content.RemoveAt(ddotIdx);
            }

            return content;
        }

        /// <summary>
        /// Get an item attribures
        /// </summary>
        /// <param name="itemFullName">The item full path name</param>
        /// <returns>A NFSAttributes class</returns>
        public NFSAttributes GetItemAttributes(string itemFullName, bool throwExceptionIfNotFoud = true)
        {
            itemFullName = CorrectPath(itemFullName);

            return _nfsInterface.GetItemAttributes(itemFullName, throwExceptionIfNotFoud);
        }

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <param name="directoryFullName">Directory full name</param>
        public void CreateDirectory(string directoryFullName)
        {
            CreateDirectory(directoryFullName, _mode);
        }

        /// <summary>
        /// Create a new directory with Permission
        /// </summary>
        /// <param name="directoryFullName">Directory full name</param>
        /// <param name="mode">Directory permissions</param>
        public void CreateDirectory(string directoryFullName, NFSPermission mode)
        {
            directoryFullName = CorrectPath(directoryFullName);

            string parentPath = System.IO.Path.GetDirectoryName(directoryFullName);

            if (!IsNullOrEmpty(parentPath) &&
                CompareOrdinal(parentPath, ".") != 0 &&
                !FileExists(parentPath))
            {
                CreateDirectory(parentPath);
            }

            _nfsInterface.CreateDirectory(directoryFullName, mode);
        }

        /// <summary>
        /// Delete a directory
        /// </summary>
        /// <param name="directoryFullName">Directory full name</param>
        public void DeleteDirectory(string directoryFullName)
        {
            DeleteDirectory(directoryFullName, true);
        }

        /// <summary>
        /// Delete a directory
        /// </summary>
        /// <param name="directoryFullName">Directory full name</param>
        public void DeleteDirectory(string directoryFullName, bool recursive)
        {
            directoryFullName = CorrectPath(directoryFullName);

            if (recursive)
            {
                foreach (string item in GetItemList(directoryFullName, true))
                {
                    if (IsDirectory($"{directoryFullName}\\{item}"))
                    { DeleteDirectory($"{directoryFullName}\\{item}", recursive); }
                    else
                    { DeleteFile($"{directoryFullName}\\{item}"); }
                }
            }

            _nfsInterface.DeleteDirectory(directoryFullName);
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="fileFullName">File full name</param>
        public void DeleteFile(string fileFullName)
        {
            fileFullName = CorrectPath(fileFullName);

            _nfsInterface.DeleteFile(fileFullName);
        }

        /// <summary>
        /// Create a new file
        /// </summary>
        /// <param name="fileFullName">File full name</param>
        public void CreateFile(string fileFullName)
        {
            CreateFile(fileFullName, _mode);
        }

        /// <summary>
        /// Create a new file with permission
        /// </summary>
        /// <param name="fileFullName">File full name</param>
        /// <param name="mode">File permission</param>
        public void CreateFile(string fileFullName, NFSPermission mode)
        {
            fileFullName = CorrectPath(fileFullName);

            _nfsInterface.CreateFile(fileFullName, mode);
        }

        /// <summary>
        /// Copy a set of files from a remote directory to a local directory
        /// </summary>
        /// <param name="sourceFileNames">A list of the remote files name</param>
        /// <param name="sourceDirectoryFullName">The remote directory path (e.g. "directory\sub1\sub2" or "." for the root)</param>
        /// <param name="destinationDirectoryFullName">The destination local directory</param>
        public void Read(IEnumerable<string> sourceFileNames, string sourceDirectoryFullName, string destinationDirectoryFullName)
        {
            if (!System.IO.Directory.Exists(destinationDirectoryFullName))
                return;
            foreach (string fileName in sourceFileNames)
            {
                Read(Combine(fileName, sourceDirectoryFullName), System.IO.Path.Combine(destinationDirectoryFullName, fileName));
            }
        }

        /// <summary>
        /// Copy a file from a remote directory to a local directory
        /// </summary>
        /// <param name="sourceFileFullName">The remote file name</param>
        /// <param name="destinationFileFullName">The destination local directory</param>
        public void Read(string sourceFileFullName, string destinationFileFullName)
        {
            System.IO.Stream fs = null;
            try
            {
                if (System.IO.File.Exists(destinationFileFullName))
                    System.IO.File.Delete(destinationFileFullName);
                fs = new System.IO.FileStream(destinationFileFullName, System.IO.FileMode.CreateNew);
                Read(sourceFileFullName, ref fs);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// Copy a file from a remote directory to a stream
        /// </summary>
        /// <param name="sourceFileFullName">The remote file name</param>
        /// <param name="outputStream"></param>
        public void Read(string sourceFileFullName, ref System.IO.Stream outputStream)
        {
            if (outputStream != null)
            {
                sourceFileFullName = CorrectPath(sourceFileFullName);

                if (!FileExists(sourceFileFullName))
                    throw new System.IO.FileNotFoundException();
                NFSAttributes nfsAttributes = GetItemAttributes(sourceFileFullName, true);
                long totalRead = nfsAttributes.Size, readOffset = 0;

                byte[] chunkBuffer = new byte[blockSize];
                int readCount, readLength = blockSize;

                do
                {
                    if (totalRead < readLength)
                    {
                        readLength = (int)totalRead;
                    }

                    readCount = _nfsInterface.Read(sourceFileFullName, readOffset, readLength, ref chunkBuffer);

                    DataEvent?.Invoke(this, new NfsEventArgs(readCount));

                    outputStream.Write(chunkBuffer, 0, readCount);

                    totalRead -= readCount; readOffset += readCount;
                }
                while (readCount != 0);

                outputStream.Flush();

                CompleteIo();
            }
            else
            {
                throw new NullReferenceException("OutputStream parameter must not be null!");
            }
        }

        /// <summary>
        /// Copy a remote file to a buffer, CompleteIO proc must called end of the reading process for system stability
        /// </summary>
        /// <param name="sourceFileFullName">The remote file full path</param>
        /// <param name="offset">Start offset</param>
        /// <param name="Count">Number of bytes</param>
        /// <param name="buffer">Output buffer</param>
        /// <returns>The number of copied bytes</returns>
        public long Read(string sourceFileFullName, long offset, long totalLenght, ref byte[] buffer)
        {
            /* This function is not suitable for large file reading.
             * Big file reading will cause OS paging creation and
             * huge memory consumption.
             */
            sourceFileFullName = CorrectPath(sourceFileFullName);

            long exactTotalLength = totalLenght - offset, currentPosition = 0;

            /* Prepare full Buffer to read */
            buffer = new byte[exactTotalLength];

            byte[] chunkBuffer = new byte[blockSize];
            int readCount = 0, readLength = blockSize;

            do
            {
                if (exactTotalLength - currentPosition < readLength)
                    readLength = (int)(exactTotalLength - currentPosition);

                readCount = _nfsInterface.Read(sourceFileFullName, offset + currentPosition, readLength, ref chunkBuffer);

                DataEvent?.Invoke(this, new NfsEventArgs(readCount));

                Array.Copy(chunkBuffer, 0, buffer, currentPosition, readCount);

                currentPosition += readCount;
            }
            while (readCount != 0);

            return currentPosition;
        }

        /// <summary>
        /// Copy a remote file to a buffer
        /// </summary>
        /// <param name="sourceFileFullName">The remote file full path</param>
        /// <param name="offset">Start offset</param>
        /// <param name="Count">Number of bytes</param>
        /// <param name="buffer">Output buffer</param>
        public void Read(string sourceFileFullName, long offset, ref long totalLenght, ref byte[] buffer)
        {
            sourceFileFullName = CorrectPath(sourceFileFullName);

            uint blockSize = (uint)this.blockSize;
            uint currentPosition = 0;
            do
            {
                uint chunkCount = blockSize;
                if (totalLenght - currentPosition < blockSize)
                    chunkCount = (uint)totalLenght - currentPosition;

                byte[] chunkBuffer = new byte[chunkCount];
                int size = _nfsInterface.Read(sourceFileFullName, offset + currentPosition, (int)chunkCount, ref chunkBuffer);

                DataEvent?.Invoke(this, new NfsEventArgs((int)chunkCount));

                if (size == 0)
                {
                    totalLenght = currentPosition;
                    return;
                }

                Array.Copy(chunkBuffer, 0, buffer, currentPosition, size);
                currentPosition += (uint)size;
            } while (currentPosition != totalLenght);
        }

        /// <summary>
        /// Copy a local file to a remote directory
        /// </summary>
        /// <param name="destinationFileFullName">The destination file full name</param>
        /// <param name="sourceFileFullName">The local full file path</param>
        public void Write(string destinationFileFullName, string sourceFileFullName)
        {
            if (System.IO.File.Exists(sourceFileFullName))
            {
                System.IO.FileStream wfs = new System.IO.FileStream(sourceFileFullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Write(destinationFileFullName, wfs);
                wfs.Close();
            }
        }

        /// <summary>
        /// Copy a local file to a remote file
        /// </summary>
        /// <param name="destinationFileFullName">The destination full file name</param>
        /// <param name="inputStream">The input file stream</param>
        public void Write(string destinationFileFullName, System.IO.Stream inputStream)
        {
            Write(destinationFileFullName, 0, inputStream);
        }

        /// <summary>
        /// Copy a local file stream to a remote file
        /// </summary>
        /// <param name="destinationFileFullName">The destination full file name</param>
        /// <param name="inputOffset">The input offset in bytes</param>
        /// <param name="inputStream">The input stream</param>
        public void Write(string destinationFileFullName, long inputOffset, System.IO.Stream inputStream)
        {
            if (inputStream != null)
            {
                destinationFileFullName = CorrectPath(destinationFileFullName);

                if (!FileExists(destinationFileFullName))
                    CreateFile(destinationFileFullName);

                long offset = inputOffset;

                byte[] buffer = new byte[blockSize];
                int readCount, writeCount;

                do
                {
                    readCount = inputStream.Read(buffer, 0, buffer.Length);

                    if (readCount != 0)
                    {
                        writeCount = _nfsInterface.Write(destinationFileFullName, offset, readCount, buffer);

                        DataEvent?.Invoke(this, new NfsEventArgs(writeCount));

                        offset += readCount;
                    }
                } while (readCount != 0);

                CompleteIo();
            }
            else
            { throw new NullReferenceException("InputStream parameter must not be null!"); }
        }

        /// <summary>
        /// Copy a local file  to a remote directory, CompleteIO proc must called end of the writing process for system stability
        /// </summary>
        /// <param name="destinationFileFullName">The full local file path</param>
        /// <param name="offset">The start offset in bytes</param>
        /// <param name="count">The number of bytes to write</param>
        /// <param name="buffer">The input buffer</param>
        public void Write(string destinationFileFullName, long offset, int count, byte[] buffer)
        {
            destinationFileFullName = CorrectPath(destinationFileFullName);

            if (!FileExists(destinationFileFullName))
                CreateFile(destinationFileFullName);

            long currentPosition = 0;

            byte[] chunkBuffer = new byte[blockSize];
            int writeCount = 0, writeLength = blockSize;

            do
            {
                if (count - currentPosition < writeLength)
                { writeLength = (int)(count - currentPosition); }

                Array.Copy(buffer, currentPosition, chunkBuffer, 0, writeLength);
                writeCount = _nfsInterface.Write(destinationFileFullName, offset + currentPosition, writeLength, chunkBuffer);

                DataEvent?.Invoke(this, new NfsEventArgs(writeCount));

                currentPosition += writeCount;
            } while (count != currentPosition);
        }

        /// <summary>
        /// Copy a local file  to a remote directory
        /// </summary>
        /// <param name="destinationFileFullName">The full local file path</param>
        /// <param name="offset">The start offset in bytes</param>
        /// <param name="count">The number of bytes</param>
        /// <param name="buffer">The input buffer</param>
        /// <returns>Returns the total written bytes</returns>
        public void Write(string destinationFileFullName, long offset, uint count, byte[] buffer, out uint totalLenght)
        {
            destinationFileFullName = CorrectPath(destinationFileFullName);

            totalLenght = count;
            uint blockSize = (uint)this.blockSize;
            uint currentPosition = 0;
            if (buffer == null)
                return;
            do
            {
                int size = -1;
                uint chunkCount = blockSize;
                if (totalLenght - currentPosition < blockSize)
                    chunkCount = (uint)totalLenght - currentPosition;

                byte[] chunkBuffer = new byte[chunkCount];
                Array.Copy(buffer, (int)currentPosition, chunkBuffer, 0, (int)chunkCount);
                size = _nfsInterface.Write(destinationFileFullName, offset + currentPosition, (int)chunkCount, chunkBuffer);
                DataEvent?.Invoke(this, new NfsEventArgs((int)chunkCount));
                if (size == 0)
                {
                    totalLenght = currentPosition;
                    return;
                }
                currentPosition += (uint)chunkCount;
            } while (currentPosition != totalLenght);
        }

        /// <summary>
        /// Move a file from/to a directory
        /// </summary>
        /// <param name="sourceFileFullName">The exact file location for source (e.g. "directory\sub1\sub2\filename" or "." for the root)</param>
        /// <param name="targetFileFullName">Target location of moving file (e.g. "directory\sub1\sub2\filename" or "." for the root)</param>
        public void Move(string sourceFileFullName, string targetFileFullName)
        {
            if (!IsNullOrEmpty(targetFileFullName))
            {
                if (targetFileFullName.LastIndexOf('\\') + 1 == targetFileFullName.Length)
                {
                    targetFileFullName = System.IO.Path.Combine(targetFileFullName, System.IO.Path.GetFileName(sourceFileFullName));
                }
            }

            sourceFileFullName = CorrectPath(sourceFileFullName);
            targetFileFullName = CorrectPath(targetFileFullName);

            _nfsInterface.Move(
                System.IO.Path.GetDirectoryName(sourceFileFullName),
                System.IO.Path.GetFileName(sourceFileFullName),
                System.IO.Path.GetDirectoryName(targetFileFullName),
                System.IO.Path.GetFileName(targetFileFullName)
            );
        }

        /// <summary>
        /// Check if the passed path refers to a directory
        /// </summary>
        /// <param name="directoryFullName">The full path (e.g. "directory\sub1\sub2" or "." for the root)</param>
        /// <returns>True if is a directory</returns>
        public bool IsDirectory(string directoryFullName)
        {
            directoryFullName = CorrectPath(directoryFullName);

            return _nfsInterface.IsDirectory(directoryFullName);
        }

        /// <summary>
        /// Completes Current Read/Write Caching and Release Resources
        /// </summary>
        public void CompleteIo() => _nfsInterface.CompleteIO();

        /// <summary>
        /// Check if a file/directory exists
        /// </summary>
        /// <param name="fileFullName">The item full name</param>
        /// <returns>True if exists</returns>
        public bool FileExists(string fileFullName)
        {
            fileFullName = CorrectPath(fileFullName);

            return GetItemAttributes(fileFullName, false) != null;
        }

        /// <summary>
        /// Get the file/directory name from a standard windwos path (eg. "\\test\text.txt" --> "text.txt" or "\\" --> ".")
        /// </summary>
        /// <param name="FullFilePath">The source path</param>
        /// <returns>The file/directory name</returns>
        public string GetFileName(string fileFullName)
        {
            fileFullName = CorrectPath(fileFullName);

            string str = System.IO.Path.GetFileName(fileFullName);
            if (IsNullOrEmpty(str))
            {
                str = ".";
            }

            return str;
        }

        /// <summary>
        /// Get the directory name from a standard windwos path (eg. "\\test\test1\text.txt" --> "test\\test1" or "\\" --> ".")
        /// </summary>
        /// <param name="fullDirectoryName">The full path(e.g. "directory/sub1/sub2" or "." for the root)</param>
        /// <returns>The directory name</returns>
        public string GetDirectoryName(string fullDirectoryName)
        {
            fullDirectoryName = CorrectPath(fullDirectoryName);

            string str = System.IO.Path.GetDirectoryName(fullDirectoryName);
            if (IsNullOrEmpty(str))
            {
                str = ".";
            }

            return str;
        }

        /// <summary>
        /// Combine a file name to a directory (eg. FileName "test.txt", Directory "test" --> "test\test.txt" or FileName "test.txt", Directory "." --> "test.txt")
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="DirectoryName">The directory name (e.g. "directory\sub1\sub2" or "." for the root)</param>
        /// <returns>The combined path</returns>
        public string Combine(string fileName, string directoryFullName)
        {
            directoryFullName = CorrectPath(directoryFullName);

            return $"{directoryFullName}\\{fileName}";
        }

        /// <summary>
        /// Set the file size
        /// </summary>
        /// <param name="fileFullName">The file full path</param>
        /// <param name="size">the size in bytes</param>
        public void SetFileSize(string fileFullName, long size)
        {
            fileFullName = CorrectPath(fileFullName);

            _nfsInterface.SetFileSize(fileFullName, size);
        }

        public static string CorrectPath(string pathEntry)
        {
            if (IsNullOrEmpty(pathEntry))
                return pathEntry;

            string[] pathList = pathEntry.Split('\\');

            pathEntry = Join("\\", pathList.Where(item => !IsNullOrEmpty(item)).ToArray());

            if (pathEntry.IndexOf('.') != 0)
            {
                pathEntry = Concat(".\\", pathEntry);
            }

            return pathEntry;
        }

        #endregion Methods
    }
}