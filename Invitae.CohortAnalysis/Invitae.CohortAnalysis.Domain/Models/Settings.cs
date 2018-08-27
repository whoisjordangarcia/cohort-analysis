using System;
namespace Invitae.CohortAnalysis.Domain.Models
{
    public class Settings
    {
        /// <summary>
        /// Gets or sets the life cycle range 
        /// used to calculate each cohort member's life cycle range in days
        /// </summary>
        /// <value>The life cycle range.</value>
        public int LifeCycleRange { get; set; }

        /// <summary>
        /// Gets or sets the cohort date format.
        /// used to format the cohort date member result
        /// </summary>
        /// <value>The cohort date format.</value>
        public string CohortDateFormat { get; set; }

        /// <summary>
        /// Gets or sets the data files folder path.
        /// </summary>
        /// <value>The data files folder path.</value>
        public string DataFilesFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the output results folder path.
        /// </summary>
        /// <value>The output results folder path.</value>
        public string OutputResultsFolderPath { get; set; }
    }
}
