package org.breeze.entity;

public class Roles {
    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column roles.id
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    private Integer id;

    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column roles.enable
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    private Integer enable;

    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column roles.name
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    private String name;

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column roles.id
     *
     * @return the value of roles.id
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public Integer getId() {
        return id;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column roles.id
     *
     * @param id the value for roles.id
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public void setId(Integer id) {
        this.id = id;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column roles.enable
     *
     * @return the value of roles.enable
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public Integer getEnable() {
        return enable;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column roles.enable
     *
     * @param enable the value for roles.enable
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public void setEnable(Integer enable) {
        this.enable = enable;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column roles.name
     *
     * @return the value of roles.name
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public String getName() {
        return name;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column roles.name
     *
     * @param name the value for roles.name
     *
     * @mbggenerated Sat Aug 11 10:36:18 CST 2012
     */
    public void setName(String name) {
        this.name = name == null ? null : name.trim();
    }
}