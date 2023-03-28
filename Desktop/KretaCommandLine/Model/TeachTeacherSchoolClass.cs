namespace KretaCommandLine.Model
{
    public class TeachTeacherSchoolClass
    {
        private long teacherId;
        private long schoolClassId;

        
        public long TeacherId { get => teacherId; set => teacherId = value; }
        public long SchoolClassId { get => schoolClassId; set => schoolClassId = value; }

        public TeachTeacherSchoolClass(long teacherId, long schoolClassId)
        {
            this.teacherId = teacherId;
            this.schoolClassId = schoolClassId;
        }

        public TeachTeacherSchoolClass()
        {
            teacherId = -1;
            schoolClassId = -1;
        }
    }
}
