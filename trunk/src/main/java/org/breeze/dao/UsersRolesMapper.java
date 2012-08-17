package org.breeze.dao;

import java.util.List;
import org.apache.ibatis.annotations.Param;
import org.breeze.entity.UsersRoles;
import org.breeze.entity.UsersRolesExample;

public interface UsersRolesMapper {
    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int countByExample(UsersRolesExample example);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int deleteByExample(UsersRolesExample example);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int deleteByPrimaryKey(Integer urid);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int insert(UsersRoles record);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int insertSelective(UsersRoles record);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    List<UsersRoles> selectByExample(UsersRolesExample example);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    UsersRoles selectByPrimaryKey(Integer urid);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int updateByExampleSelective(@Param("record") UsersRoles record, @Param("example") UsersRolesExample example);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int updateByExample(@Param("record") UsersRoles record, @Param("example") UsersRolesExample example);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int updateByPrimaryKeySelective(UsersRoles record);

    /**
     * This method was generated by MyBatis Generator.
     * This method corresponds to the database table users_roles
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    int updateByPrimaryKey(UsersRoles record);
}