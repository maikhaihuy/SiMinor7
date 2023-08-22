export interface RoleMapping {
    [key: string]: string;
  }
  export const roleKeys = {
    Administrator: 'Administrator',
    Moderator: 'Moderator'
  };
  
  export const Roles = {
    Administrator: 'Administrator',
    Moderator: 'Moderator'
  };
  
  export const roleMapping: RoleMapping = {
    [roleKeys.Administrator]: 'Administrator',
    [roleKeys.Moderator]: 'Moderator'
  };
  
  export const RoleList = [Roles.Administrator, Roles.Moderator];
  
  export default roleMapping;
  