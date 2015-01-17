using System;

namespace Business.Manager
{
    public static class CommonUtility
    {
        /// <summary>
        ///     This routine calculates the distance between two points (given the latitude/longitude of those points). It is being
        ///     used to calculate the distance between two locations using GeoDataSource(TM) products
        /// </summary>
        /// <param name="unit">
        ///     where: 'M' is statute miles
        ///     'K' is kilometers (default)
        ///     'N' is nautical miles
        /// </param>
        /// <param name="lat1">First Latitude</param>
        /// <param name="lon1">First Longitude</param>
        /// <param name="lat2">Second Latitud</param>
        /// <param name="lon2">Second Longitude</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(GetRadFromDegree(lat1))*Math.Sin(GetRadFromDegree(lat2)) + Math.Cos(GetRadFromDegree(lat1))*Math.Cos(GetRadFromDegree(lat2))*Math.Cos(GetRadFromDegree(theta));
            dist = Math.Acos(dist);
            dist = GetDegreeFromRad(dist);
            dist = dist*60*1.1515;
            if (unit == 'K')
            {
                dist = dist*1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist*0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double GetRadFromDegree(double deg)
        {
            return (deg*Math.PI/180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double GetDegreeFromRad(double rad)
        {
            return (rad/Math.PI*180.0);
        }
    }
}