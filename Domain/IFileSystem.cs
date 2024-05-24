﻿namespace Domain;

public interface IFileSystem
{
  IEnumerable<string> EnumerateFiles(string path);
  StreamReader OpenFile(string path);

  IEnumerable<string> LoadFilesFromPath(string folderPath);
}