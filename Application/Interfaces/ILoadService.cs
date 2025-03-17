﻿using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

// интерфейс сервиса для работы с загрузкой/выгрузкой файлов
public interface ILoadService
{
    // добавление уникального суффикса к имени файла изображения
    string GetUniqueFileName(string fileName);


    // получить путь к файлу аватарки пользователя
    string GetPathToUserAvatar(string fileName);

    // получить путь к временному файлу аватарки пользователя
    string GetPathToTempUserAvatar(int userId, string fileName);


    // получить путь к файлу изображения компании
    string GetPathToCompanyImage(string imageType, string fileName);

    // получить путь к временному файлу изображения компании
    string GetPathToTempCompanyImage(
        string imageType, string tempDir, string fileName);


    // получить имя временной папки для хранения
    // фотографий пользователя с учётом его идентификатора
    string GetTempUserPhotoDirectoryById(int userId);


    // получить имя временной папки для хранения
    // изображений компании с учётом типа изображения
    // и идентификаторов пользователя и компании
    string GetTempCompanyImageDirectoryByParams(
        string imageType, int userId, int companyId);


    // копирование файла
    void CopyFile(string srcFilePath, string dscFilePath);


    // удаление всех файлов в заданной папке
    void DeleteFilesByPath(string path);


    // удаление папки с содержимым
    (bool, string) DeleteDirectory(string directoryPath);


    // загрузка файлов (Upload)

    // 1. загрузка файла на сервер
    Task UploadFileAsync(string path, string fileName, IFormFile file);


    // выгрузка файлов (Download)

    // 1. сформировать специальный объект ответа на запрос
    // для передачи файла как массива байтов
    /*Task<FileContentResult>
        DownloadFileAsBytes(string path, string fileName);*/

} // interface ILoadService