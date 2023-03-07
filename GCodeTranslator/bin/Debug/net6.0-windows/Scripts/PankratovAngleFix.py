import math
import os
import sys
import traceback
from time import sleep

angle_critical_difference: float = 0
max_angle_value: float = 0
directory_path: str = ""

manual_start_flag = False


def wait_until_manual_closing():
    print("Зафиксируйте при необходимости ошибку и вручную закройте окно")
    while True:
        sleep(1)

try:

    def manual_start():
        global angle_critical_difference
        global max_angle_value
        global directory_path

        print('Enter angle_critical_difference in degrees: ')
        angle_critical_difference = float(input())
        print('Enter max angle value:')
        max_angle_value = float(input())

        #  Работаем с файловой системой, отбираем файлы, которые необходимо пропускать через скрипт
        print('Enter needed directory path: ')
        directory_path = input()  # вводим путь к папке со слоями


    def cmd_start():
        global angle_critical_difference
        global max_angle_value
        global directory_path
        if len(sys.argv) != 4:
            raise ValueError("Неверное количество аргументов команной строки")
        else:
            directory_path = sys.argv[1]
            max_angle_value = float(sys.argv[2])
            angle_critical_difference = float(sys.argv[3])
        print(f"directory_path: {directory_path}\n"
              f"max_angle_value: {max_angle_value}\n"
              f"angle_critical_difference: {angle_critical_difference}\n")


    if manual_start_flag:
        manual_start()
    else:
        cmd_start()

    files_list = os.listdir(directory_path)  # сохраняем имена файлов из папки
    needed_layer_files = []

    for i in range(len(files_list)):
        file_extension = ''
        file_name = files_list[i]
        file_name_length = len(file_name)
        for j in range(file_name_length - 2, file_name_length):
            file_extension = file_extension + file_name[j]
        if file_extension == 'ls':
            needed_layer_files.append(directory_path + '\\' + file_name)

    print('The following files will be modified: ', needed_layer_files)

    for a in range(len(needed_layer_files)):

        # НАЧАЛЬНОЕ ОБЪЯВЛЕНИЕ КООРДИНАТ
        angle_w_str = ''  # переменные для сохранения значений текущих углов формата str (начальное объявление не убирать)
        angle_p_str = ''
        angle_r_str = ''

        flag_for_first_point = -1  # флаг нахождения первой точки
        flag_if_angle_is_found = -1  # флаг нахождения строки с углом
        flag_if_p_is_found = -1  # флаг нахождения строки с P[i] во второй части ls файла
        flag_for_needed_p_line = -1  # флаг нахождения нужной строки с P[i] из начала файла для добавления новой во 2 фазе проги
        flag_coordinates = -1  # флаг нахождения строки с координатами XYZ

        line_modified = ''  # str переменная для записи измененных строк в модифицированный файл
        line_extra = ''  # str переменная новых строк
        line_coordinates = ''  # строка под координаты

        p_addition_counter = 0  # счетчик. отвечает за то, насколько увеличивать индекс последующих точек, когда будем добавлять промежуточные
        p_counter = 0  # счетчик точек

        p_dict = {}  # словарь для добавления джампов к точкам. Слово - номер точки P, ключ - количество добавленных после него точек

        file_name = needed_layer_files[a]
        file = open(file_name, 'r')
        file_extra_name = file_name.replace('.ls', '_extra.ls')
        file_extra = open(file_extra_name, 'w+')

        while True:
            line = file.readline()
            if not line:
                break
            angle_flag = line.find('W = ')
            if angle_flag != -1:
                line_length = len(line)  # запоминаем значения углов первой точки
                w_first_index = angle_flag + 4  # запоминаем расположение значений углов
                w_second_index = line.find(' deg')
                p_first_index = line.find('P = ') + 4
                p_second_index = line.find(' deg', p_first_index, line_length)
                r_first_index = line.find('R = ') + 4
                r_second_index = line.find(' deg', r_first_index, line_length)
                for i in range(w_first_index, w_second_index):  # записываем значения углов
                    angle_w_str = angle_w_str + line[i]
                for i in range(p_first_index, p_second_index):
                    angle_p_str = angle_p_str + line[i]
                for i in range(r_first_index, r_second_index):
                    angle_r_str = angle_r_str + line[i]

                angle_w = float(angle_w_str)
                angle_p = float(angle_p_str)
                angle_r = float(angle_r_str)

                angle_w_str = ''
                angle_p_str = ''
                angle_r_str = ''

                list_of_angles = [angle_w, angle_p, angle_r]
                for i in range(3):
                    if list_of_angles[i] > max_angle_value:
                        list_of_angles[i] = max_angle_value
                    elif list_of_angles[i] < -max_angle_value:
                        list_of_angles[i] = -max_angle_value
                line_extra = '       W = ' + str(list_of_angles[0]) + ' deg, P = ' + str(list_of_angles[1]) + ' deg, R = ' + str(
                    list_of_angles[2]) + ' deg \n'
                file_extra.write(line_extra)
            else:
                file_extra.write(line)

        file.close()
        file_extra.close()
        delete_file_name = needed_layer_files[a]
        os.remove(delete_file_name)
        file_extra = open(file_extra_name, 'r+')
        file_modified_name = file_extra_name.replace('_extra.ls', '_modified.ls')  # создаем новый модифицированный файл на запись
        file_modified = open(file_modified_name, 'w+')

        #  НАХОЖДЕНИЕ ПЕРВОЙ ТОЧКИ
        while True:  # цикл, чтобы скипнуть первую часть с перечислением точек и перейти к части с координатами и углами
            line = file_extra.readline()
            flag_for_first_point = line.find('W = ')
            file_modified.write(line)
            if flag_for_first_point != -1:
                p_counter = p_counter + 1
                break

        # ЗАПОМИНАНИЕ УГЛОВ ПЕРВОЙ ТОЧКИ
        line_length = len(line)  # запоминаем значения углов первой точки (FIXME!!! костыль, чтобы не было out of boundaries)
        w_first_index = flag_for_first_point + 4  # запоминаем расположение значений углов FIXME!: В ФУНКЦИЮ ОБЕРНУТЬ
        w_second_index = line.find(' deg')
        p_first_index = line.find('P = ') + 4
        p_second_index = line.find(' deg', p_first_index, line_length)
        r_first_index = line.find('R = ') + 4
        r_second_index = line.find(' deg', r_first_index, line_length)
        for i in range(w_first_index, w_second_index):  # записываем значения углов
            angle_w_str = angle_w_str + line[i]
        for i in range(p_first_index, p_second_index):
            angle_p_str = angle_p_str + line[i]
        for i in range(r_first_index, r_second_index):
            angle_r_str = angle_r_str + line[i]

        #  ПОСТРОЧНОЕ ЧТЕНИЕ И МОДИФИКАЦИЯ ФАЙЛА
        while True:  # читаем файл построчно
            line = file_extra.readline()
            if not line:
                break  # выходим из цикла чтения файла, если больше нет строк
            flag_coordinates = line.find(
                'X = ')  # сохраняем строчку с координатами, чтобы потом пописать ее в новой точке, ее нельзя захардкодить FIXME!!!: сохранять в отдельные переменные коорды
            if flag_coordinates != -1:
                line_coordinates = line
                flag_coordinates = -1
            flag_if_angle_is_found = line.find('W = ')
            if flag_if_angle_is_found != -1:  # если нашли строчку с углами
                previous_angle_w_str = angle_w_str  # записываем углы предыдущей точки
                previous_angle_p_str = angle_p_str
                previous_angle_r_str = angle_r_str
                angle_w_str = ''
                angle_p_str = ''
                angle_r_str = ''
                line_length = len(line)
                w_first_index = line.find('W = ') + 4  # запоминаем расположение углов этой точки
                w_second_index = line.find(' deg')
                p_first_index = line.find('P = ') + 4
                p_second_index = line.find(' deg', p_first_index, line_length)
                r_first_index = line.find('R = ') + 4
                r_second_index = line.find(' deg', r_first_index, line_length)

                for i in range(w_first_index, w_second_index):  # записываем значения углов
                    angle_w_str = angle_w_str + line[i]
                for i in range(p_first_index, p_second_index):
                    angle_p_str = angle_p_str + line[i]
                for i in range(r_first_index, r_second_index):
                    angle_r_str = angle_r_str + line[i]

                angle_w = float(
                    angle_w_str)  # конвертим строки в значения для последующей проверки FIXME!!!: в цикл оберни, чудовище
                angle_p = float(angle_p_str)
                angle_r = float(angle_r_str)

                previous_angle_w = float(previous_angle_w_str)
                previous_angle_p = float(previous_angle_p_str)
                previous_angle_r = float(previous_angle_r_str)

                if abs(angle_w - previous_angle_w) > angle_critical_difference or abs(
                        angle_p - previous_angle_p) > angle_critical_difference or abs(
                    angle_r - previous_angle_r) > angle_critical_difference:  # если приращение хотя бы одного из трех углов выше критического значения - создаем промежуточную точку
                    difference_w = abs(angle_w - previous_angle_w)
                    difference_p = abs(angle_p - previous_angle_p)
                    difference_r = abs(angle_r - previous_angle_r)
                    list_of_differences = [difference_w, difference_p, difference_r]
                    max_angle_difference = max(list_of_differences)
                    number_of_iterations = math.ceil(max_angle_difference / angle_critical_difference)
                    if number_of_iterations == 1:

                        addition_w = (angle_w - previous_angle_w) / 2
                        addition_p = (angle_p - previous_angle_w) / 2
                        addition_r = (angle_r - previous_angle_w) / 2
                        line_extra = '       W = ' + str(round(angle_w - addition_w, 2)) + ' deg, P = ' + str(
                            round(angle_p - addition_p, 2)) + ' deg, R = ' + str(round(angle_r - addition_r, 2)) + ' deg \n'
                        file_modified.write(line_extra)
                        line_extra = '    GP2:\n'
                        file_modified.write(line_extra)
                        line_extra = '        UF : 1, UT : 2,\n'
                        file_modified.write(line_extra)
                        line_extra = '       J1 = 0.0 deg, J2 = 0.0 deg \n'
                        file_modified.write(line_extra)
                        line_extra = ' };\n'
                        file_modified.write(line_extra)
                        p_addition_counter = p_addition_counter + 1
                        line_extra = 'P[' + str(p_counter + p_addition_counter) + '] {\n'
                        file_modified.write(line_extra)
                        line_extra = '    GP1:\n'
                        file_modified.write(line_extra)
                        line_extra = "        UF : 6, UT : 5,     CONFIG: 'N U T, 0, 0, 0',\n"
                        file_modified.write(line_extra)
                        file_modified.write(line_coordinates)
                        p_dict[p_counter - 1] = 1
                    else:
                        addition_w = (angle_w - previous_angle_w) / number_of_iterations  # вместо деления пополам будет k потом
                        addition_p = (angle_p - previous_angle_p) / number_of_iterations
                        addition_r = (angle_r - previous_angle_r) / number_of_iterations
                        for j in range(number_of_iterations, 1, -1):
                            line_extra = '       W = ' + str(round(angle_w - (j - 1) * addition_w, 2)) + ' deg, P = ' + str(
                                round(angle_p - (j - 1) * addition_p, 2)) + ' deg, R = ' + str(
                                round(angle_r - (j - 1) * addition_r, 2)) + ' deg \n'
                            file_modified.write(line_extra)
                            line_extra = '    GP2:\n'
                            file_modified.write(line_extra)
                            line_extra = '        UF : 1, UT : 2,\n'
                            file_modified.write(line_extra)
                            line_extra = '       J1 = 0.0 deg, J2 = 0.0 deg \n'
                            file_modified.write(line_extra)
                            line_extra = ' };\n'
                            file_modified.write(line_extra)
                            p_addition_counter = p_addition_counter + 1
                            line_extra = 'P[' + str(p_counter + p_addition_counter) + '] {\n'
                            file_modified.write(line_extra)
                            line_extra = '    GP1:\n'
                            file_modified.write(line_extra)
                            line_extra = "        UF : 6, UT : 5,     CONFIG: 'N U T, 0, 0, 0',\n"
                            file_modified.write(line_extra)
                            file_modified.write(line_coordinates)
                        p_dict[p_counter - 1] = number_of_iterations - 1

                    line_modified = line
                else:
                    line_modified = line
                flag_if_angle_is_found = -1  # обнуление флага
            else:
                line_modified = line

            flag_if_p_is_found = line.find('P[')  # чтобы счетчик точек корректно прописывался
            if flag_if_p_is_found != -1:
                p_counter = p_counter + 1
                line_modified = 'P[' + str(p_counter + p_addition_counter) + '] {\n'
                flag_if_p_is_found = -1
            file_modified.write(line_modified)

        #  СОЗДАЕМ ФИНАЛЬНЫЙ ФАЙЛ

        file_modified.close()
        file_extra.close()
        delete_file_name = file_extra_name
        os.remove(delete_file_name)
        file_modified = open(file_modified_name, 'r+')
        file_final_name = file_modified_name.replace('_modified.ls',
                                                     '_final.ls')  # создаем финальный модифицированный файл на запись
        file_final = open(file_final_name, 'w+')
        flag_if_p_is_found = -1
        current_p_line = ''
        p_add_count = 0
        is_next_line_an_arc = -1
        is_line_already_written = 1
        previous_line = ''
        line_extra = ''
        flag_for_p1_jump = -1
        flag_first_time_in_cycle = -1
        weave_flag = -1

        # ОБРАБАТЫВАЕМ ДЖАМПЫ К ТОЧКАМ В НАЧАЛЕ И ФОРМИРУЕМ ФИНАЛЬНЫЙ ФАЙЛ

        while True:  # добегаем до P[1] чтобы обработать отдельно
            line = file_modified.readline()
            flag_for_p1_jump = line.find('1: L P[1]')
            file_final.write(line)
            if flag_for_p1_jump != -1:
                break

        speed_flag = line.find('cm/min')

        index1 = flag_for_p1_jump + 7  # обрабатываем P[1] отдельно
        index2_str = line.find(']')
        index2 = index2_str
        for i in range(index1, index2):
            current_p_line = current_p_line + line[i]
        current_p_line_int = int(current_p_line)
        current_p_line = ''
        p_line = p_dict.get(current_p_line_int, -1)
        if p_line != -1:
            if speed_flag != -1:
                line = file_modified.readline()
                file_final.write(line)
                line = file_modified.readline()
                weave_flag = line.find(': Weave Sine')
                if weave_flag != - 1:
                    file_final.write(line)
                    line = file_modified.readline()
                current_p_line_int = 2
                for i in range(p_line):
                    p_add_count += 1
                    file_final.write(str(current_p_line_int + p_add_count - 1) + ': L P[' + str(
                        current_p_line_int + p_add_count - 1) + '] WELD_SPEED CNT100 COORD PTH ;\n')
                file_final.write(str(current_p_line_int + p_add_count) + ': L P[' + str(
                    current_p_line_int + p_add_count) + '] WELD_SPEED CNT100 COORD PTH ;\n')
            else:
                line = file_modified.readline()
                current_p_line_int = 2
                for i in range(p_line):
                    p_add_count += 1
                    file_final.write(str(current_p_line_int + p_add_count - 1) + ': L P[' + str(
                        current_p_line_int + p_add_count - 1) + '] WELD_SPEED CNT100 COORD PTH ;\n')
                file_final.write(str(current_p_line_int + p_add_count) + ': L P[' + str(
                    current_p_line_int + p_add_count) + '] WELD_SPEED CNT100 COORD PTH ;\n')

        p_dict_length = len(p_dict)  # основной цикл обработки джампов
        current_p_line_int = 3
        while True:
            previous_line = line
            line = file_modified.readline()
            if not line:
                break
            flag_if_p_is_found = line.find('L P[')
            if flag_if_p_is_found != -1:
                index1 = flag_if_p_is_found + 4
                index2_str = line.find(']')
                index2 = index2_str
                for i in range(index1, index2):
                    current_p_line = current_p_line + line[i]
                current_p_line_int = int(current_p_line)
                current_p_line = ''
                p_line = p_dict.get(current_p_line_int - 1, -1)
                if p_line != -1:
                    line_extra_index = line.find(']')
                    line_length = len(line)
                    for j in range(line_extra_index, line_length):
                        line_extra = line_extra + line[j]
                    for i in range(p_line):
                        p_add_count += 1
                        line_to_write = str(current_p_line_int + p_add_count - 1) + ': L P[' + str(
                            current_p_line_int + p_add_count - 1) + line_extra
                        file_final.write(line_to_write)
                    current_p_line = ''
                    line_to_write = str(current_p_line_int + p_add_count) + ': L P[' + str(
                        current_p_line_int + p_add_count) + line_extra
                    file_final.write(line_to_write)
                    line_extra = ''
                else:
                    line_extra_index = line.find(']')
                    line_length = len(line)
                    for j in range(line_extra_index, line_length):
                        line_extra = line_extra + line[j]
                    line_to_write = str(current_p_line_int + p_add_count) + ': L P[' + str(
                        current_p_line_int + p_add_count) + line_extra
                    file_final.write(line_to_write)
                    line_extra = ''

            else:
                file_final.write(line)

        print('Dictionary: ', p_dict)

        file_modified.close()
        file_final.close()
        delete_file_name = file_modified_name
        os.remove(delete_file_name)

        p_counter = 0
        p_add_count = 0
        flag_if_p_is_found = -1
        current_p_line = ''
        current_coordinates = [1, 1, 1]
        previous_coordinates = [1, 1, 1]
        x_str = ''
        y_str = ''
        z_str = ''
        is_it_first_point = 0

        #  ПОДПРАВЛЯЕМ КООРДИНАТЫ
        file_completed_name = file_final_name.replace('_final.ls', '_completed.ls')
        file_completed = open(file_completed_name, 'w+')
        file_final = open(file_final_name, 'r+')

        while True:  # Мотаем до позиций
            line = file_final.readline()
            file_completed.write(line)
            flag_if_p_is_found = line.find('/POS')
            if flag_if_p_is_found != -1:
                break

        while True:
            line = file_final.readline()
            if not line:
                break
            flag_if_p_is_found = line.find('P[')
            if flag_if_p_is_found != -1:
                p_counter += 1
                # вычленяем номер точки
                index1 = flag_if_p_is_found + 2
                index2_str = line.find(']')
                index2 = index2_str
                for i in range(index1, index2):
                    current_p_line = current_p_line + line[i]
                current_p_line_int = int(current_p_line)
                # проверяем делались ли после нее доп точки
                p_line_original = current_p_line_int - p_add_count
                p_line = p_dict.get(p_line_original, -1)
                current_p_line = ''
                if p_line != -1:
                    for i in range(3):
                        file_completed.write(line)
                        line = file_final.readline()
                    # заносим координаты точки после которой идут дополнительный в current
                    line_length = len(line)
                    x_first_index = line.find('X = ') + 4
                    x_second_index = line.find(' mm')
                    y_first_index = line.find('Y = ') + 4
                    y_second_index = line.find(' mm', y_first_index, line_length)
                    z_first_index = line.find('Z = ') + 4
                    z_second_index = line.find(' mm', z_first_index, line_length)

                    for i in range(x_first_index, x_second_index):  # записываем значения углов
                        x_str = x_str + line[i]
                    for i in range(y_first_index, y_second_index):
                        y_str = y_str + line[i]
                    for i in range(z_first_index, z_second_index):
                        z_str = z_str + line[i]

                    float_x = float(x_str)
                    float_y = float(y_str)
                    float_z = float(z_str)

                    current_coordinates[0] = float_x
                    current_coordinates[1] = float_y
                    current_coordinates[2] = float_z

                    x_str = ''
                    y_str = ''
                    z_str = ''

                    for i in range(3):
                        previous_coordinates[i] = current_coordinates[i]
                    for i in range(9):
                        file_completed.write(line)
                        line = file_final.readline()
                    p_counter += 1

                    # читаем координаты первой доп точки чтобы вычислить addition
                    line_length = len(line)
                    x_first_index = line.find('X = ') + 4
                    x_second_index = line.find(' mm')
                    y_first_index = line.find('Y = ') + 4
                    y_second_index = line.find(' mm', y_first_index, line_length)
                    z_first_index = line.find('Z = ') + 4
                    z_second_index = line.find(' mm', z_first_index, line_length)

                    for i in range(x_first_index, x_second_index):  # записываем значения углов
                        x_str = x_str + line[i]
                    for i in range(y_first_index, y_second_index):
                        y_str = y_str + line[i]
                    for i in range(z_first_index, z_second_index):
                        z_str = z_str + line[i]

                    float_x = float(x_str)
                    float_y = float(y_str)
                    float_z = float(z_str)

                    current_coordinates[0] = float_x
                    current_coordinates[1] = float_y
                    current_coordinates[2] = float_z

                    x_str = ''
                    y_str = ''
                    z_str = ''

                    difference_x = current_coordinates[0] - previous_coordinates[0]
                    difference_y = current_coordinates[1] - previous_coordinates[1]
                    difference_z = current_coordinates[2] - previous_coordinates[2]

                    addition_x = difference_x / (p_line + 1)
                    addition_y = difference_y / (p_line + 1)
                    addition_z = difference_z / (p_line + 1)

                    current_coordinates[0] = previous_coordinates[0] + addition_x
                    current_coordinates[1] = previous_coordinates[1] + addition_y
                    current_coordinates[2] = previous_coordinates[2] + addition_z
                    line_coordinates = '       X = ' + str(round(current_coordinates[0], 2)) + ' mm, Y = ' + str(
                        round(current_coordinates[1], 2)) + ' mm, Z = ' + str(round(current_coordinates[2], 2)) + ' mm,\n'

                    file_completed.write(line_coordinates)
                    if p_line == 1:
                        for i in range(4):
                            line = file_final.readline()
                            file_completed.write(line)
                        line = file_final.readline()
                    else:
                        for i in range(5):
                            line = file_final.readline()
                            file_completed.write(line)
                    for i in range(p_line - 1):
                        for j in range(3):
                            line = file_final.readline()
                            file_completed.write(line)
                        for j in range(3):
                            previous_coordinates[j] = current_coordinates[j]
                        current_coordinates[0] = previous_coordinates[0] + addition_x
                        current_coordinates[1] = previous_coordinates[1] + addition_y
                        current_coordinates[2] = previous_coordinates[2] + addition_z
                        line_coordinates = '       X = ' + str(round(current_coordinates[0], 2)) + ' mm, Y = ' + str(
                            round(current_coordinates[1], 2)) + ' mm, Z = ' + str(round(current_coordinates[2], 2)) + ' mm,\n'
                        file_completed.write(line_coordinates)
                        line = file_final.readline()
                        for j in range(4):
                            line = file_final.readline()
                            file_completed.write(line)
                        line = file_final.readline()
                        if i != p_line - 2:
                            file_completed.write(line)
                        p_counter += 1
                    p_add_count += p_line
                else:
                    line_modified = line

            else:
                line_modified = line
            line_modified = line
            file_completed.write(line_modified)

        file_final.close()
        file_completed.close()

        delete_file_name = file_final_name
        os.remove(delete_file_name)
        rename_file_name = needed_layer_files[a]
        os.rename(file_completed_name, rename_file_name)

        print(needed_layer_files[a], 'has been successfully modified!')

    print("done!")
    sleep(3)
except Exception:
    traceback.print_exc()
    wait_until_manual_closing()
