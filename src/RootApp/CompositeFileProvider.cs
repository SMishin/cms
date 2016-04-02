﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.FileProviders;
using Microsoft.Extensions.Primitives;

namespace CMS
{
	public class CompositeFileProvider : IFileProvider
	{
		private readonly IEnumerable<IFileProvider> _fileProviders;

		public CompositeFileProvider(params IFileProvider[] fileProviders)
		{
			this._fileProviders = fileProviders;
		}

		public IDirectoryContents GetDirectoryContents(string subpath)
		{
			foreach (IFileProvider fileProvider in this._fileProviders)
			{
				IDirectoryContents directoryContents = fileProvider.GetDirectoryContents(subpath);

				if (directoryContents != null && directoryContents.Exists)
					return directoryContents;
			}

			return new NonexistentDirectoryContents();
		}

		public IFileInfo GetFileInfo(string subpath)
		{
			foreach (IFileProvider fileProvider in this._fileProviders)
			{
				IFileInfo fileInfo = fileProvider.GetFileInfo(subpath);

				if (fileInfo != null && fileInfo.Exists)
					return fileInfo;
			}

			return new NonexistentFileInfo(subpath);
		}

		public IChangeToken Watch(string filter)
		{
			foreach (IFileProvider fileProvider in this._fileProviders)
			{
				IChangeToken changeToken = fileProvider.Watch(filter);

				if (changeToken != null)
					return changeToken;
			}

			return NonexistentChangeToken.Singleton;
		}
	}

	internal class NonexistentDirectoryContents : IDirectoryContents
	{
		public bool Exists
		{
			get
			{
				return false;
			}
		}

		public IEnumerator<IFileInfo> GetEnumerator()
		{
			return Enumerable.Empty<IFileInfo>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}

	internal class NonexistentFileInfo : IFileInfo
	{
		private readonly string name;

		public NonexistentFileInfo(string name)
		{
			this.name = name;
		}

		public bool Exists
		{
			get
			{
				return false;
			}
		}

		public bool IsDirectory
		{
			get
			{
				return false;
			}
		}

		public DateTimeOffset LastModified
		{
			get
			{
				return DateTimeOffset.MinValue;
			}
		}

		public long Length
		{
			get
			{
				return -1;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public string PhysicalPath
		{
			get
			{
				return null;
			}
		}

		public Stream CreateReadStream()
		{
			throw new FileNotFoundException(this.name);
		}
	}

	internal class NonexistentChangeToken : IChangeToken
	{
		public static NonexistentChangeToken Singleton { get; } = new NonexistentChangeToken();

		public bool ActiveChangeCallbacks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool HasChanged
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IDisposable RegisterChangeCallback(Action<object> callback, object state)
		{
			throw new NotImplementedException();
		}


	}
}
