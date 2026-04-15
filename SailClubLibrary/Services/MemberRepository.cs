using Microsoft.Data.SqlClient;
using SailClubLibrary.Data;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Member Repository Objects using the interface
    /// </summary>
    public class MemberRepository : Connection, IMemberRepositoryAsync
    {
        #region Instance Fields
        private Dictionary<string, Member> _members;
        private string _queryString = "SELECT * FROM SailClubMember";
        private string _queryCount = "SELECT Count(*) FROM SailClubMember";
        private string _querySearch = "SELECT * FROM SailClubMember WHERE MemberId=@ID";
        private string _insertSql = "INSERT INTO SailClubMember (FirstName, SurName, PhoneNumber, MemberAddress, City, Mail, MemberType, MemberRole, MemberImage) Values(@FirstName,@SurName,@PhoneNumber,@MemberAddress,@City,@Mail,@MemberType,@MemberRole,@MemberImage)";
        private string _deleteSql = "DELETE FROM SailClubMember WHERE MemberId=@ID";
        private string _updateSql = "UPDATE SailClubMember SET FirstName=@FirstName, SurName=@SurName, PhoneNumber=@PhoneNumber, MemberAddress=@MemberAddress, City=@City, Mail=@Mail, MemberType=@MemberType, MemberRole=@MemberRole, MemberImage=@MemberImage WHERE MemberId=@ID";
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public MemberRepository()
        {
            //_members = new Dictionary<string, Member>();
            //_members = new MockData().MemberData;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for getting the number of members in database asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<int> Count()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryCount, connection);
                    await command.Connection.OpenAsync();
                    return (int)await command.ExecuteScalarAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return 0;
            }
        }

        /// <summary>
        /// Method for adding members to database.
        /// </summary>
        /// <param name="member">The member to add to database</param>
        public async Task AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@SurName", member.SurName);
                    command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
                    command.Parameters.AddWithValue("@MemberAddress", member.Address);
                    command.Parameters.AddWithValue("@City", member.City);
                    command.Parameters.AddWithValue("@Mail", member.Mail);
                    command.Parameters.AddWithValue("@MemberType", (int)member.TheMemberType);
                    command.Parameters.AddWithValue("@MemberRole", (int)member.TheMemberRole);
                    if (member.MemberImage != null)
                    {
                        command.Parameters.AddWithValue("@MemberImage", member.MemberImage);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@MemberImage", DBNull.Value);
                    }
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw;
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// Method for returning a list of members from database
        /// </summary>
        public async Task<List<Member>> GetAllMembers()
        {
            List<Member> foundMembers = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string firstName = reader.GetString("FirstName");
                        string surName = reader.GetString("SurName");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string memberAddress = reader.GetString("MemberAddress");
                        string city = reader.GetString("City");
                        string mail = reader.GetString("Mail");
                        MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("MemberType")];
                        MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("MemberRole")];
                        Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                        member.MemberImage = reader.GetString("MemberImage");
                        foundMembers.Add(member);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }

            return foundMembers;
        }

        /// <summary>
        /// Method for removing a member from the database
        /// </summary>
        /// <param name="member">The member to remove from database</param>
        public async Task RemoveMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", member.Id);
                    await command.ExecuteNonQueryAsync();

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
        }
        
        /// <summary>
        /// Method to update a member's info in database
        /// </summary>
        public async Task UpdateMember(Member updatedMember)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", updatedMember.Id);
                    command.Parameters.AddWithValue("@PhoneNumber", updatedMember.PhoneNumber);
                    command.Parameters.AddWithValue("@FirstName", updatedMember.FirstName);
                    command.Parameters.AddWithValue("@SurName", updatedMember.SurName);
                    command.Parameters.AddWithValue("@MemberAddress", updatedMember.Address);
                    command.Parameters.AddWithValue("@City", updatedMember.City);
                    command.Parameters.AddWithValue("@Mail", updatedMember.Mail);
                    command.Parameters.AddWithValue("@MemberType", (int)updatedMember.TheMemberType);
                    command.Parameters.AddWithValue("@MemberRole", (int)updatedMember.TheMemberRole);
                    command.Parameters.AddWithValue("@MemberImage", updatedMember.MemberImage);

                    await command.ExecuteNonQueryAsync();

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// Searches through the member database and returns the member with the given id. 
        /// </summary>
        public async Task<Member?> SearchMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Member member = new Member();
                try
                {
                    SqlCommand command = new SqlCommand(_querySearch, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string firstName = reader.GetString("FirstName");
                        string surName = reader.GetString("SurName");
                        string memberAddress = reader.GetString("MemberAddress");
                        string city = reader.GetString("City");
                        string mail = reader.GetString("Mail");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("MemberType")];
                        MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("MemberRole")];
                        member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                        member.MemberImage = reader.GetString("MemberImage");
                        reader.Close();
                    }
                    return member;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return null;
            }
        }

        /// <summary>
        /// Method for printing the info of every member in the dictionary
        /// </summary>
        public async Task PrintAll()
        {
            foreach (Member member in _members.Values)
            {
                Console.WriteLine(member);
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Asynchronously retrieves all members from the database, optionally filtering and sorting the results based
        /// on the specified criteria.
        /// </summary>
        /// <param name="filterByProperty">The name of the member property to filter by. Must be one of the allowed column names: "MemberId",
        /// "FirstName", "SurName", "PhoneNumber", "MemberAddress", "City", or "Mail".</param>
        /// <param name="filterCriteria">The value to filter the specified property by. If null, no filtering is applied for the property.</param>
        /// <param name="theMemberType">An optional member type to filter the results. If specified, only members of the given type are returned.</param>
        /// <param name="sortColumn">The name of the column to sort the results by. Must be one of the allowed column names. If not valid,
        /// defaults to "MemberId".</param>
        /// <param name="sortOrder">The sort direction for the results. Specify "asc" for ascending or "desc" for descending order. If not
        /// "asc", descending order is used.</param>
        /// <returns>A list of members matching the specified filter and sort criteria. The list is empty if no members are
        /// found.</returns>
        /// <exception cref="ArgumentException">Thrown if the value of filterByProperty is not a valid column name.</exception>
        public async Task<List<Member>> GetAllMembers(string filterByProperty, string filterCriteria, MemberType? theMemberType, string sortColumn, string sortOrder)
        {
            List<string> allowedColumns = new List<string>
            {
                "MemberId", "FirstName", "SurName", "PhoneNumber", "MemberAddress", "City", "Mail"
            };
            if (!allowedColumns.Contains(filterByProperty))
            {
                throw new ArgumentException("Invalid filter column");
            }

            if (!allowedColumns.Contains(sortColumn))
            {
                sortColumn = "MemberId"; // default
            }

            List<Member> foundMembers = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = _queryString;
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    if (filterCriteria != null)
                    {
                        query += $" WHERE {filterByProperty} like @filter";
                        command.Parameters.AddWithValue("@filter", $"%{filterCriteria}%");
                        if (theMemberType != null)
                        {
                            query += $" AND MemberType = @memberType";
                            command.Parameters.AddWithValue("@memberType", (int)theMemberType);
                        }
                    }
                    else if (theMemberType != null)
                    {
                        query += $" WHERE MemberType = @memberType";
                        command.Parameters.AddWithValue("@memberType", (int)theMemberType);
                    }
                    if (sortColumn != null)
                    {
                        query += $" ORDER BY {sortColumn} {(sortOrder == "asc" ? "ASC" : "DESC")}";
                    }

                    command.CommandText = query;

                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string firstName = reader.GetString("FirstName");
                        string surName = reader.GetString("SurName");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string memberAddress = reader.GetString("MemberAddress");
                        string city = reader.GetString("City");
                        string mail = reader.GetString("Mail");
                        MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("MemberType")];
                        MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("MemberRole")];
                        Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                        member.MemberImage = reader.GetString("MemberImage");
                        foundMembers.Add(member);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw;
                }
            }
            return foundMembers;
        }


        public async Task<List<Member>> FilterMembers(string filterByProperty, string filterCriteria, MemberType? memberType)
        {
            IEnumerable<Member> members = await GetAllMembers();
            var filteredList = members.Where(m => m.TheMemberType == (memberType ?? m.TheMemberType));
            var filter = new FilterByProperty<Member>(filterByProperty, filterCriteria);
            filteredList = filteredList.Where(m => filter.IsMatch(m));

            return filteredList.ToList();
        }

        /* switch case filtering
        public List<Member> FilterMembers(string filterCriteria, string filterByProperty, MemberType memberType)
        {
            List<Member> filteredList = new List<Member>();

            foreach (Member m in _members.Values.Where(m => m.TheMemberType == memberType))
            {
                switch (filterByProperty)
                {
                    case "FirstName":
                        if (m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "SurName":
                        if (m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "PhoneNumber":
                        if (m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Address":
                        if (m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Mail":
                        if (m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "City":
                        if (m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Id":
                        if (m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    default:
                        if (m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                        {
                            filteredList.Add(m);
                        }
                        break;
                }
            }
            return filteredList;
        }
        */

        /*
        public List<Member> FilterMembers(string filterCriteria, string filterByProperty, MemberType memberType)
        {
            var filteredList = _members.Values.Where(m => m.TheMemberType == memberType);
            if (!string.IsNullOrEmpty(filterByProperty))
            {
                PropertyInfo propertyInfo = typeof(Member).GetProperty(filterByProperty);
                if (propertyInfo != null)
                {
                    filteredList = filteredList.Where(m =>
                    {
                        var value = propertyInfo.GetValue(m).ToString();
                        return value != null && value.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase);
                    });
                }
            }
            else
            {
                filteredList = filteredList.Where(m =>
                    m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase));
            }
            return filteredList.ToList();
        }
        */



        #endregion
    }
}
