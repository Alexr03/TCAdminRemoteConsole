create table tcmodule_terminal_scripts
(
    scriptId     int auto_increment
        primary key,
    name         text not null,
    contents     text not null,
    terminalType int  not null
);

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url, controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar, view_order, required_permissions, menu_required_permissions, page_manager, page_search_provider, cache_name) VALUES (1040, '07405876-e8c2-4b24-a774-4ef57f596384', null, null, 3, '/RemoteConsole', '/RemoteConsole', 'RemoteConsole', 'Index', 'Remote Console', 'MenuIcons/Base/ServerComponents24x24.png', 'MenuIcons/Base/ServerComponents16x16.png', 1, 1000, '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', null, null, null);