﻿using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;
using System;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for photos.
    /// </summary>
    public class PhotosDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public PhotosDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public PhotosDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Gets all photos.
        /// </summary>
        /// <returns>All photos collection.</returns>
        public List<Photo> GetAllPhotos()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                List<Photo> returned = (from o in context.Photos select o).ToList();
                foreach (var p in returned)
                {
                    p.Tags.Clear();
                    p.Tags = FillTags(context, p.ID);
                }

                return returned;
            }
        }

        /// <summary>
        /// Gets the last photo.
        /// </summary>
        /// <returns>Last photo entity.</returns>
        public Photo GetLastPhoto()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<Photo> query = (from o in context.Photos select o);

                if (query.Count() == 0)
                {
                    return null;
                }
                else
                {
                    Photo p = query.Last();
                    p.Tags = FillTags(context, p.ID);
                    return p;
                }
            }
        }

        /// <summary>
        /// Fills the tags.
        /// </summary>
        /// <param name="context">The Context.</param>
        /// <param name="id">Photo ID.</param>
        private List<Tag> FillTags(PhotoCollectionDatabaseEntities context, int id)
        {
            return (from o in context.Tags
                    join t2p in context.Tags2PhotosSet on o.ID equals t2p.TagID
                    where t2p.PhotoID == id
                    select o).Distinct().ToList();
        }

        /// <summary>
        /// Get one photo.
        /// </summary>
        /// <param name="id">Photo ID.</param>
        /// <returns>One photo.</returns>
        public Photo GetPhoto(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                Photo p = context.Photos.Where(a => a.ID == id).First();
                p.Tags = FillTags(context, id);
                return p;
            }
        }

        /// <summary>
        /// Adding new photo into database.
        /// </summary>
        /// <param name="newPhoto">New photo entity.</param>
        /// <returns>ID for created photo.</returns>
        public int AddPhoto(Photo newPhoto)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newPhoto.ID = context.Photos.NextId(p => p.ID);
                newPhoto.Date = DateTime.Now;

                context.Photos.AddObject(newPhoto);
                context.SaveChanges();

                return newPhoto.ID;
            }
        }

        /// <summary>
        /// Adding photos by group.
        /// </summary>
        /// <param name="photos">List of photos.</param>
        /// <returns>Row count for added group.</returns>
        public int AddPhotosGroup(List<Photo> photos)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                foreach (Photo each in photos)
                {
                    each.ID = context.Photos.NextId(p => p.ID);
                    context.Photos.AddObject(each);
                }

                return context.SaveChanges();
            }
        }

        /// <summary>
        /// Deleting photos by id.
        /// </summary>
        /// <param name="id">Photo ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeletePhoto(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.Photos.DeleteObject((from o in context.Photos where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Deleting photos by list of ids.
        /// </summary>
        /// <param name="ids">List of ids.</param>
        /// <returns>Row count number.</returns>
        public int DeletePhotos(List<int> ids)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                foreach (int id in ids)
                {
                    foreach(var tag2photo in (from o in context.Tags2PhotosSet where o.PhotoID == id select o))
                    {
                        context.Tags2PhotosSet.DeleteObject(tag2photo);
                    }

                    context.Photos.DeleteObject((from o in context.Photos where o.ID == id select o).First());
                }

                return context.SaveChanges();
            }
        }
    }
}